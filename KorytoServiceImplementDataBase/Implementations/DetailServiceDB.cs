using KorytoModel;
using KorytoServiceDAL.BindingModel;
using KorytoServiceDAL.Interfaces;
using KorytoServiceDAL.ViewModel;
using KorytoServiceImplementDataBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KorytoMaxinKuznetsovServiceDB.Implementations
{
    public class DetailServiceDB : IDetailService
    {
        AbstractDbContext context;

        public DetailServiceDB(AbstractDbContext context)
        {
            this.context = context;
        }

        public void AddElement(DetailBindingModel model)
        {
            Detail detail = context.Details.FirstOrDefault(
                record => record.DetailName == model.DetailName);

            if (detail != null)
            {
                throw new Exception("Уже есть деталь");
            }

            context.Details.Add(new Detail
            {
                DetailName = model.DetailName,
                TotalAmount = model.TotalAmount
            });

            context.SaveChanges();
        }

        public void DeleteElement(int id)
        {
            Detail detail = context.Details.FirstOrDefault(
                record => record.Id == id);

            if (detail != null)
            {
                context.Details.Remove(detail);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Деталь не найдена");
            }
        }

        public DetailViewModel GetElement(int id)
        {
            Detail detail = context.Details.FirstOrDefault(
                record => record.Id == id);

            if (detail != null)
            {
                return new DetailViewModel
                {
                    Id = detail.Id,
                    DetailName = detail.DetailName,
                    TotalAmount = detail.TotalAmount
            };

            }
            throw new Exception("Деталь не найдена");
        }

        public List<DetailViewModel> GetList()
        {
            List<DetailViewModel> result = context.Details.Select(record => new DetailViewModel
            {
                Id = record.Id,
                DetailName = record.DetailName,
                TotalAmount = record.TotalAmount
            }).ToList();

            return result;
        }

        public void UpdateElement(DetailBindingModel model)
        {
            Detail detail = context.Details.FirstOrDefault(
                record => record.DetailName == model.DetailName && record.Id != model.Id);

            if (detail != null)
            {
                throw new Exception("Уже есть деталь");
            }

            detail = context.Details.FirstOrDefault(
                rec => rec.Id == model.Id);

            if (detail == null)
            {
                throw new Exception("Деталь не найдена");
            }

            detail.DetailName = model.DetailName;
            detail.TotalAmount = model.TotalAmount;

            context.SaveChanges();
        }
    }
}
