using KorytoModel;
using KorytoServiceDAL.BindingModel;
using KorytoServiceDAL.Interfaces;
using KorytoServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KorytoServiceImplementDataBase.Implementations
{
    public class ClientServiceDB : IClientService
    {
        private readonly KorytoDbContext context;

        public ClientServiceDB(KorytoDbContext context)
        {
            this.context = context;
        }

        public void AddElement(ClientBindingModel model)
        {
            var element = context.Clients.FirstOrDefault(rec => rec.ClientFIO == model.ClientFIO);

            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }

            context.Clients.Add(new Client
            {
                ClientFIO = model.ClientFIO,
                Login = model.Login,
                Mail = model.Mail,
                Password = model.Password
            });
            context.SaveChanges();
        }

        public void DeleteElement(int id)
        {
            var element = context.Clients.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Clients.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public ClientViewModel GetElement(int id)
        {
            var element = context.Clients.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ClientViewModel
                {
                    Id = element.Id,
                    ClientFIO = element.ClientFIO,
                    Login = element.Login,
                    Mail = element.Mail,
                    Password = element.Password
                };
            }
            throw new Exception("Элемент не найден");
        }

        public List<ClientViewModel> GetList()
        {
            return context.Clients.Select(rec => new ClientViewModel
            {
                Id = rec.Id,
                ClientFIO = rec.ClientFIO,
                Mail = rec.Mail,
                Login = rec.Login,
                Password = rec.Password
            }).ToList();
        }

        public void UpdateElement(ClientBindingModel model)
        {
            var element = context.Clients.FirstOrDefault(rec => rec.ClientFIO == model.ClientFIO && rec.Id != model.Id);

            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }

            element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            element.ClientFIO = model.ClientFIO;
            element.Mail = model.Mail;
            element.Login = model.Login;
            element.Password = model.Password;
            context.SaveChanges();
        }
    }
}
