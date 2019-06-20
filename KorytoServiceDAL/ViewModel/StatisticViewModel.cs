namespace KorytoServiceDAL.ViewModel
{
    public class StatisticViewModel
    {
        public (string, int) MostPopularCar { get; set; }

        public (string, int) MostPopularCarClient { get; set; }

        public decimal AverageCheck { get; set; }

        public decimal AverageCheckClient { get; set; }

        public int CountCarsClient { get; set; }
    }
}
