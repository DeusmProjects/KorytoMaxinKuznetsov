using KorytoModel;
using KorytoServiceDAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

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
            return context.Orders
                .Where(order => order.ClientId == clientId)
                .Sum(order => order.OrderCars.Sum(x => x.Amount));
        }

        public decimal AverageCustomerCheck(int clientId)
        { 
            return context.Orders
               .Where(order => order.ClientId == clientId)
               .Average(order => order.TotalSum);
        }


        public string PopularCar(int clientId)
        {
            var clientCars = context.OrderCars.Where(rec => rec.Order.ClientId == clientId).Select(rec => rec);

            var most = clientCars
                .GroupBy(rec => rec.CarId)
                .Select(rec => new { Id = rec.Key, Total = rec.Sum(x => x.Amount) })
                .OrderByDescending(rec => rec.Total)
                .FirstOrDefault();

            if(most != null)
            {
                var name = context.Cars.FirstOrDefault(rec => rec.Id == most.Id)?.CarName;

                var count = most.Total;

                return name + " в количестве " + count + " машин"; ;
            }
            else
            {
                return "У Вас не было заказов";
            }
        }

        public decimal GetAverPrice()
        { 
            return context.Orders.Average(order => order.TotalSum);
        }
    }
}
