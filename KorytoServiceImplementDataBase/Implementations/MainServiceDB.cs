using KorytoModel;
using KorytoServiceDAL.BindingModel;
using KorytoServiceDAL.Interfaces;
using KorytoServiceDAL.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;

namespace KorytoServiceImplementDataBase.Implementations
{
    public class MainServiceDB : IMainService
    {
        private readonly KorytoDbContext context;

        public MainServiceDB(KorytoDbContext context)
        {
            this.context = context;
        }

        public void CreateOrder(OrderBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var element = new Order
                    {
                        ClientId = model.ClientId,
                        DateCreate = DateTime.Now,
                        TotalSum = model.TotalSum,
                        OrderStatus = OrderStatus.Принят
                    };
                    context.Orders.Add(element);
                    context.SaveChanges();

                    var groupCars = model.OrderCars
                        .GroupBy(rec => rec.CarId)
                        .Select(rec => new { CarId = rec.Key, Amount = rec.Sum(r => r.Amount) });

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
            var element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            if (element.OrderStatus != OrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }

            element.DateImplement = DateTime.Now;
            element.OrderStatus = OrderStatus.Готов;
            context.SaveChanges();
        }

        public List<OrderViewModel> GetClientOrders(int clientId)
        {
            return GetList().Where(order => order.ClientId == clientId).ToList();
        }

        public List<OrderViewModel> GetList()
        {
            var result = context.Orders.Select(rec => new OrderViewModel
            {
                Id = rec.Id,
                ClientId = rec.ClientId,
                DateCreate =
                    SqlFunctions.DateName("dd", rec.DateCreate) + " " + SqlFunctions.DateName("mm", rec.DateCreate) +
                    " " + SqlFunctions.DateName("yyyy", rec.DateCreate),
                DateImplement =
                    rec.DateImplement == null
                        ? ""
                        : SqlFunctions.DateName("dd", rec.DateImplement.Value) + " " +
                          SqlFunctions.DateName("mm", rec.DateImplement.Value) + " " +
                          SqlFunctions.DateName("yyyy", rec.DateImplement.Value),
                StatusOrder = rec.OrderStatus.ToString(),
                TotalSum = rec.TotalSum,
                ClientFIO = rec.Client.ClientFIO,
                OrderCars = context.OrderCars.Where(recPC => recPC.OrderId == rec.Id)
                    .Select(recPC => new OrderCarViewModel
                    {
                        Id = recPC.Id,
                        CarId = recPC.CarId,
                        OrderId = recPC.OrderId,
                        CarName = recPC.Car.CarName,
                        Amount = recPC.Amount
                    })
                    .ToList()
            })
                .ToList();

            return result;
        }

        public OrderViewModel GetElement(int id)
        {
            var element = context.Orders.FirstOrDefault(rec => rec.Id == id);

            if (element != null)
            {
                return new OrderViewModel
                {
                    Id = element.Id,
                    ClientId = element.ClientId,
                    ClientFIO = context.Clients.FirstOrDefault(client => client.Id == element.ClientId)?.ClientFIO,
                    TotalSum = element.TotalSum,
                    StatusOrder = element.OrderStatus.ToString(),
                    DateCreate = element.DateCreate.ToString(CultureInfo.InvariantCulture),
                    DateImplement = element.DateImplement.ToString(),
                    OrderCars = context.OrderCars.Where(recOC => recOC.OrderId == element.Id)
                        .Select(recOC => new OrderCarViewModel
                        {
                            Id = recOC.Id,
                            OrderId = recOC.OrderId,
                            CarId = recOC.CarId,
                            CarName = recOC.Car.CarName,
                            Amount = recOC.Amount
                        })
                        .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void PayOrder(OrderBindingModel model)
        {
            var element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
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
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var element = new Order
                    {
                        ClientId = model.ClientId,
                        DateCreate = DateTime.Now,
                        TotalSum = model.TotalSum,
                        OrderStatus = OrderStatus.Зарезервирован
                    };

                    context.Orders.Add(element);
                    context.SaveChanges();

                    var groupCars = model.OrderCars
                        .GroupBy(rec => rec.CarId)
                        .Select(rec => new {CarId = rec.Key, Amount = rec.Sum(r => r.Amount)});

                    foreach (var groupCar in groupCars)
                    {
                        var orderCar = new OrderCar
                        {
                            OrderId = element.Id,
                            CarId = groupCar.CarId,
                            Amount = groupCar.Amount
                        };

                        context.OrderCars.Add(orderCar);

                        var carDetails = context.CarDetails.Where(rec => rec.CarId == orderCar.CarId);

                        if (carDetails.All(rec =>
                            rec.Amount <= context.Details.FirstOrDefault(r => r.Id == rec.DetailId).TotalAmount))
                        {
                            foreach (var carDetail in carDetails)
                            {
                                var detail = context.Details.FirstOrDefault(r => r.Id == carDetail.DetailId);

                                detail.TotalAmount -= carDetail.Amount;
                                detail.TotalReserve += carDetail.Amount;
                            }
                        }
                        else
                        {
                            throw new Exception("Недостаточно деталей для резервации");
                        }

                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }

        }

        public void TakeOrderInWork(OrderBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);

                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }

                    if (element.OrderStatus == OrderStatus.Принят || element.OrderStatus == OrderStatus.Зарезервирован)
                    {
                        var orderCars = context.OrderCars.Where(rec => rec.OrderId == element.Id);

                        foreach (var orderCar in orderCars)
                        {
                            var carDetails = context.CarDetails.Where(rec => rec.CarId == orderCar.CarId);
                            foreach (var carDetail in carDetails)
                            {
                                if (element.OrderStatus == OrderStatus.Принят)
                                {
                                    var countDetails = context.Details
                                        .FirstOrDefault(detail => detail.Id == carDetail.DetailId).TotalAmount;

                                    if (carDetail.Amount > countDetails)
                                    {
                                        throw new Exception("Недостаточно деталей");
                                    }
                                    else
                                    {
                                        context.Details
                                            .FirstOrDefault(detail => detail.Id == carDetail.DetailId).TotalAmount -= carDetail.Amount;

                                        context.SaveChanges();
                                        break;
                                    }
                                }

                                if (element.OrderStatus != OrderStatus.Зарезервирован) continue;
                                {
                                    int countDetails = carDetail.Detail.TotalReserve;

                                    if (carDetail.Amount > countDetails)
                                    {
                                        throw new Exception("Недостаточно деталей");
                                    }
                                    else
                                    {
                                        carDetail.Detail.TotalReserve -= carDetail.Amount;
                                        context.SaveChanges();
                                        break;
                                    }
                                }
                            }
                        }

                        element.OrderStatus = OrderStatus.Выполняется;
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    else
                    {
                        throw new Exception("Заказ не в статусе \"Принят\" или \"Зарезервирован\"");
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void SaveDataBase()
        {
            SaveEntity(context.Orders.ToList());
            SaveEntity(context.Cars.ToList());
            SaveEntity(context.Clients.ToList());
            SaveEntity(context.Details.ToList());
            SaveEntity(context.OrderCars.ToList());
            SaveEntity(context.CarDetails.ToList());
            SaveEntity(context.DetailRequests.ToList());
            SaveEntity(context.Requests.ToList());
        }

        private static void SaveEntity(IEnumerable entity)
        {
            var jsonFormatter = new DataContractJsonSerializer(entity.GetType());

            using (var fs = new FileStream($"backup/{GetNameEntity(entity)}.json",
                FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, entity);
            }
        }

        private static string GetNameEntity(IEnumerable entity)
        {
            return entity.AsQueryable().ElementType.ToString().Split('.')[1];
        }
    }
}
