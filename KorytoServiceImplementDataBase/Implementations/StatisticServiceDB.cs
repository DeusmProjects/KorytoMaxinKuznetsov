using KorytoServiceDAL.Interfaces;
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

        public string GetMostPopularCar()
        {
            var most = context.OrderCars
                .GroupBy(rec => rec.CarId)
                .Select(rec => new { Id = rec.Key, Total = rec.Sum(x => x.Amount)})
                .OrderByDescending(rec => rec.Total)
                .First();

            string name = context.Cars.FirstOrDefault(rec => rec.Id == most.Id).CarName;

            int count = most.Total;

            return name + " в количестве " + count + " машин";
        }
    }
}
