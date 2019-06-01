using KorytoModel;
using KorytoServiceDAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace KorytoServiceImplementDataBase.Implementations
{
    public class StatisticServiceDB : IStatistic
    {
        private KorytoDbContext context;

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

            string name = context.Cars.FirstOrDefault(rec => rec.Id == most.Id).CarName;

            int count = most.Total;

            return (name, count);
        }

        public int HowManyCarTheClient(int clientId)
        {
            int summary = 0;

            Client client = context.Clients.FirstOrDefault(rec => rec.Id == clientId);

            List<Order> ordersClient = context.Orders.Where(rec => rec.ClientId == client.Id).Select(rec => rec).ToList();

            foreach (var orderClient in ordersClient)
            {
                List<OrderCar> orderCars = orderClient.OrderCars;

                foreach (var orderCar in orderCars)
                {
                    summary += orderCar.Amount;
                }
            }

            return summary;
        }

        public decimal AverageCustomerCheck(int clientId)
        {
            decimal summary = 0;

            Client client = context.Clients.FirstOrDefault(rec => rec.Id == clientId);

            List<Order> ordersClient = context.Orders.Where(rec => rec.ClientId == client.Id).Select(rec => rec).ToList();

            foreach (var orderClient in ordersClient)
            {
                summary += orderClient.TotalSum;
            }

            return summary / ordersClient.Count;
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
                string name = context.Cars.FirstOrDefault(rec => rec.Id == most.Id).CarName;

                int count = most.Total;

                return name + " в количестве " + count + " машин"; ;
            }
            else
            {
                return "У Вас не было заказов";
            }
        }

        public decimal GetAverPrice()
        { 
            return context.Orders.Sum(rec => rec.TotalSum) / context.Orders.Count();
        }
    }
}
