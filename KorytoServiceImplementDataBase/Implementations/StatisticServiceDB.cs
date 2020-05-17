using System;
using System.Collections.Generic;
using KorytoServiceDAL.Interfaces;
using System.Linq;
using KorytoServiceDAL.ViewModel;

namespace KorytoServiceImplementDataBase.Implementations
{
    public class StatisticServiceDB : IStatisticService
    {
        private readonly KorytoDbContext context;

        public StatisticServiceDB(KorytoDbContext context)
        {
            this.context = context;
        }

        public (string name, int count) GetMostPopularCar()
        {
            var most = context.OrderCars
                .GroupBy(rec => rec.CarId)
                .Select(rec => new { Id = rec.Key, Total = rec.Sum(x => x.Amount)})
                .OrderByDescending(rec => rec.Total)
                .First();

            var name = context.Cars.FirstOrDefault(rec => rec.Id == most.Id)?.CarName;

            var count = most.Total;

            return (name, count);
        }

        public int GetClientCarsCount(int clientId)
        {
            int clientCars = context.Orders
                .Count(order => order.ClientId == clientId);

            if (clientCars != 0)
            {
                return context.Orders
                    .Where(order => order.ClientId == clientId)
                    .Sum(order => order.OrderCars.Sum(x => x.Amount));
            }
            else
            {
                return 0;
            }
        }

        public decimal GetAverageCustomerCheck(int clientId)
        {
            int clientCars = context.Orders
                .Count(order => order.ClientId == clientId);

            if (clientCars != 0)
            {
                return context.Orders
                    .Where(order => order.ClientId == clientId)
                    .Average(order => order.TotalSum);
            }
            else
            {
                return 0;
            }
        }


        public (string name, int count) GetPopularCarClient(int clientId)
        {
            var most = context.OrderCars
                .Where(rec => rec.Order.ClientId == clientId)
                .GroupBy(rec => rec.CarId)
                .Select(rec => new { Id = rec.Key, Total = rec.Sum(x => x.Amount) })
                .OrderByDescending(rec => rec.Total)
                .FirstOrDefault();

            if (most != null)
            {
                var name = context.Cars.FirstOrDefault(rec => rec.Id == most.Id)?.CarName;

                var count = most.Total;

                return (name, count);
            }
            else
            {
                return (name: null, count: 0);
            }
        }

        public decimal GetAverageCheck()
        { 
            return context.Orders.Average(order => order.TotalSum);
        }

        public List<CarCountViewModel> GetCarStatistic()
        {
            var data = new List<CarCountViewModel>();

            var cars = context.OrderCars
                .GroupBy(rec => context.Cars.FirstOrDefault(r => r.Id == rec.CarId).CarName)
                .Select(rec => new {Name = rec.Key, Total = rec.Sum(x => x.Amount)})
                .OrderByDescending(rec => rec.Total);

            foreach (var car in cars)
            {
                data.Add(new CarCountViewModel
                {
                    CarName = car.Name,
                    CarCount = car.Total
                });
            }

            return data;
        }
    }
}
