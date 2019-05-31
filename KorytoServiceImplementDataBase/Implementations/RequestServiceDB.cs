using KorytoModel;
using KorytoServiceDAL.BindingModel;
using KorytoServiceDAL.Interfaces;
using KorytoServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KorytoServiceImplementDataBase.Implementations
{
    public class RequestServiceDB : IRequestService
    {

        KorytoDbContext context;

        public RequestServiceDB(KorytoDbContext context)
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
                        DetailRequest detailRequest = new DetailRequest
                        {
                            RequestId = request.Id,
                            DetailId = gr.detailId,
                            Amount = gr.amount
                        };

                        context.DetailRequests.Add(detailRequest);
                        context.SaveChanges();

                        Detail updateDetail = context.Details.FirstOrDefault(record => record.Id == detailRequest.DetailId);

                        if (updateDetail != null)
                        {
                            updateDetail.TotalAmount += detailRequest.Amount;
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
                    transaction.Commit();
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
    }
}
