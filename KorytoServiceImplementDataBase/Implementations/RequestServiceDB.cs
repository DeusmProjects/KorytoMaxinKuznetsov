using KorytoModel;
using KorytoServiceDAL.BindingModel;
using KorytoServiceDAL.Interfaces;
using KorytoServiceDAL.ViewModel;
using KorytoServiceImplementDataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace KorytoMaxinKuznetsovServiceDB.Implementations
{
    public class RequestServiceDB : IRequestService
    {

        AbstractDbContext context;

        public RequestServiceDB(AbstractDbContext context)
        {
            this.context = context;
        }

        public void AddElement(RequestBindingModel model)
        {

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {

                    Request request = new Request
                    {
                        DateCreate = model.DateCreate
                    };

                    context.Requests.Add(request);
                    context.SaveChanges();

                    var groupDetails = model.DetailRequests.GroupBy(record => record.DetailId)
                        .Select(record => new
                        {
                            detailId = record.Key,
                            amount = record.Sum(r => r.Amount)
                        });

                    foreach (var gr in groupDetails)
                    {
                        context.DetailRequests.Add(new DetailRequest
                        {
                            RequestId = gr.detailId,
                            DetailId = gr.detailId,
                            Amount = gr.amount
                        });
                        context.SaveChanges();
                    }

                    transaction.Commit();
                }

                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }

            }
        }

        public void DeleteElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Request request = context.Requests.FirstOrDefault(record => record.Id == id);

                    if (request != null)
                    {
                        context.DetailRequests.RemoveRange(context.DetailRequests.Where(rec => rec.RequestId == id));

                        context.Requests.Remove(request);

                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Заявка не найдена");
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public RequestViewModel GetElement(int id)
        {
            Request request = context.Requests.FirstOrDefault(
                record => record.Id == id);

            if (request != null)
            {
                return new RequestViewModel
                {
                    Id = request.Id,
                    DateCreate = request.DateCreate,
                    DetailRequests = context.DetailRequests
                        .Where(record => record.RequestId == request.Id)
                            .Select(recPC => new DetailRequestViewModel
                            {
                                Id = recPC.Id,
                                DetailId = recPC.DetailId,
                                RequestId = recPC.RequestId,
                                Amount = recPC.Amount
                            }).ToList()
                };

            }
            throw new Exception("Заявка не найдена");
        }

        public List<RequestViewModel> GetList()
        {
            List<RequestViewModel> result = context.Requests.Select(record => new RequestViewModel
            {
                Id = record.Id,
                DateCreate = record.DateCreate,

                DetailRequests = context.DetailRequests.Where(r => r.RequestId == record.Id).Select(r => new DetailRequestViewModel
                {
                    Id = r.Id,
                    DetailId = r.DetailId,
                    RequestId = r.RequestId,
                    Amount = r.Amount
                }).ToList()

            }).ToList();

            return result;
        }

        public void UpdateElement(RequestBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {

                    Request request = context.Requests.FirstOrDefault(
                        rec => rec.Id != model.Id);
                    
                    if (request == null)
                    {
                        throw new Exception("Элемент не найден");
                    }

                    request.DateCreate = model.DateCreate;
                   
                    context.SaveChanges();



                    var detailId = model.DetailRequests.Select(rec => rec.DetailId).Distinct();

                    var updateDetails = context.DetailRequests.Where(rec => rec.RequestId == model.Id && detailId.Contains(rec.DetailId));

                    foreach (var detail in updateDetails)
                    {

                        detail.Amount = model.DetailRequests.FirstOrDefault(rec => rec.Id == detail.Id).Amount;

                    }

                    context.SaveChanges();

                    context.DetailRequests.RemoveRange(context.DetailRequests
                        .Where(rec => rec.Id == model.Id && !detailId.Contains(rec.DetailId)));

                    context.SaveChanges();

                    var groupDetails = model.DetailRequests.Where(rec => rec.Id == 0).GroupBy(rec => rec.DetailId).Select(rec => new
                    {
                        detail_ID = rec.Key,
                        amount = rec.Sum(r => r.Amount)
                    });

                    foreach (var groupDetail in groupDetails)
                    {
                        DetailRequest detail = context.DetailRequests.FirstOrDefault(rec => rec.RequestId == model.Id && rec.DetailId == groupDetail.detail_ID);

                        if (detail != null)
                        {
                            detail.Amount += groupDetail.amount;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.DetailRequests.Add(new DetailRequest
                            {
                                RequestId = model.Id,
                                DetailId = groupDetail.detail_ID,
                                Amount = groupDetail.amount
                            });
                            context.SaveChanges();
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
