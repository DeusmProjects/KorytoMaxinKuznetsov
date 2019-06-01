using KorytoModel;
using KorytoServiceDAL.BindingModel;
using KorytoServiceDAL.Interfaces;
using KorytoServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KorytoServiceImplementDataBase.Implementations
{
    public class DetailServiceDB : IDetailService
    {
        readonly KorytoDbContext context;

        public DetailServiceDB(KorytoDbContext context)
        {
            this.context = context;
        }

        public void AddElement(DetailBindingModel model)
        {
            var detail = context.Details.FirstOrDefault(record => record.DetailName == model.DetailName);

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
            var detail = context.Details.FirstOrDefault(
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
            var detail = context.Details.FirstOrDefault(record => record.Id == id);

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
            return context.Details.Select(record => new DetailViewModel
            {
                Id = record.Id,
                DetailName = record.DetailName,
                TotalAmount = record.TotalAmount
            }).ToList();
        }

        public void UpdateElement(DetailBindingModel model)
        {
            var detail = context.Details.FirstOrDefault(record => record.DetailName == model.DetailName && record.Id != model.Id);

            if (detail != null)
            {
                throw new Exception("Уже есть деталь");
            }

            detail = context.Details.FirstOrDefault(rec => rec.Id == model.Id);

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
