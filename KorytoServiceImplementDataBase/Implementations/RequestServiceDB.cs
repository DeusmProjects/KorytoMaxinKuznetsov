using KorytoModel;
using KorytoServiceDAL.BindingModel;
using KorytoServiceDAL.Interfaces;
using KorytoServiceDAL.ViewModel;
using KorytoServiceImplementDataBase;
using System;
using System.Collections.Generic;

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
            throw new NotImplementedException();
        }

        public void DeleteElement(int id)
        {
            throw new NotImplementedException();
        }

        public RequestViewModel GetElement(int id)
        {
            throw new NotImplementedException();
        }

        public List<RequestViewModel> GetList()
        {
            throw new NotImplementedException();
        }

        public void UpdateElement(RequestBindingModel model)
        {
            throw new NotImplementedException();
        }
    }
}
