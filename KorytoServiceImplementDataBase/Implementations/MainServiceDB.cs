﻿using KorytoModel;
using KorytoServiceDAL.BindingModel;
using KorytoServiceDAL.Interfaces;
using KorytoServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;

namespace KorytoServiceImplementDataBase.Implementations
{
    public class MainServiceDB : IMainService
    {
        private AbstractDbContext context;

        public MainServiceDB(AbstractDbContext context)
        {
            this.context = context;
        }

        public void CreateOrder(OrderBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Order element = new Order
                    {
                        ClientId = model.ClientId,
                        DateCreate = DateTime.Now,
                        TotalSum = model.TotalSum,
                        OrderStatus = OrderStatus.Принят
                    };
                    context.Orders.Add(element);
                    context.SaveChanges();
                    // убираем дубли по машинам
                    var groupCars = model.OrderCars
                     .GroupBy(rec => rec.CarId)
                    .Select(rec => new
                    {
                        CarId = rec.Key,
                        Amount = rec.Sum(r => r.Amount)
                    });
                    // добавляем компоненты
                    foreach (var groupCar in groupCars)
                    {
                        context.OrderCars.Add(new OrderCar
                        {
                            OrderId = element.Id,
                            CarId = groupCar.CarId,
                            Amount = groupCar.Amount
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

        public void FinishOrder(OrderBindingModel model)
        {
            Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.OrderStatus != OrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.OrderStatus = OrderStatus.Готов;
            context.SaveChanges();
        }

        public List<OrderViewModel> GetList()
        {
            List<OrderViewModel> result = context.Orders.Select(rec => new OrderViewModel
            {
                Id = rec.Id,
                ClientId = rec.ClientId,
                DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
            SqlFunctions.DateName("mm", rec.DateCreate) + " " +
            SqlFunctions.DateName("yyyy", rec.DateCreate),
                DateImplement = rec.DateImplement == null ? "" :
            SqlFunctions.DateName("dd",
           rec.DateImplement.Value) + " " +
            SqlFunctions.DateName("mm",
           rec.DateImplement.Value) + " " +
            SqlFunctions.DateName("yyyy",
            rec.DateImplement.Value),
                StatusOrder = rec.OrderStatus.ToString(),
                TotalSum = rec.TotalSum,
                ClientFIO = rec.Client.ClientFIO,
                OrderCars = context.OrderCars
                .Where(recPC => recPC.OrderId == rec.Id)
                .Select(recPC => new OrderCarViewModel
                {
                    Id = recPC.Id,
                    CarId = recPC.CarId,
                    OrderId = recPC.OrderId,
                    CarName = recPC.Car.CarName,
                    Amount = recPC.Amount
                }).ToList()
            }).ToList();

            return result;
        }

        public void PayOrder(OrderBindingModel model)
        {
            Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.OrderStatus != OrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.OrderStatus = OrderStatus.Оплачен;
            context.SaveChanges();
        }

        public void ReserveOrder(OrderBindingModel model)
        {
            throw new NotImplementedException();
        }

        public void TakeOrderInWork(OrderBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    if (element.OrderStatus != OrderStatus.Принят)
                    {
                        throw new Exception("Заказ не в статусе \"Принят\"");
                    }
                    var carDetails = context.CarDetails.Include(rec => rec.Detail).Where(rec => rec.CarId == element.);
                    // списываем
                    foreach (var productCondition in productConditions)
                    {
                        int countOnCitys = productCondition.Amount * element.Amount;
                        var stockConditions = context.CityConditions.Where(rec =>
                        rec.ConditionId == productCondition.ConditionId);
                        foreach (var stockCondition in stockConditions)
                        {
                            // компонентов на одном слкаде может не хватать
                            if (stockCondition.Amount >= countOnCitys)
                            {
                                stockCondition.Amount -= countOnCitys;
                                countOnCitys = 0;
                                context.SaveChanges();
                                break;
                            }
                            else
                            {
                                countOnCitys -= stockCondition.Amount;
                                stockCondition.Amount = 0;
                                context.SaveChanges();
                            }
                        }
                        if (countOnCitys > 0)
                        {
                            throw new Exception("Не достаточно условий " +
                           productCondition.Condition.ConditionName + " требуется " + productCondition.Amount + ", не хватает " + countOnCitys);
                        }
                    }
                    element.DateImplement = DateTime.Now;
                    element.OrderStatus = OrderStatus.Выполняется;
                    context.SaveChanges();
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