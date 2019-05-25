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
            context.Requests.Add(new Request
            {
                DateCreate = model.DateCreate
            });

            context.SaveChanges();
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
                DateCreate = record.DateCreate

            }).ToList();

            return result;
        }

        public void UpdateElement(RequestBindingModel model)
        {
            throw new NotImplementedException();
        }
    }
}
