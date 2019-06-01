namespace KorytoServiceDAL.Interfaces
{
    public interface IStatistic
    {
        string GetMostPopularCar();

        decimal AverageCustomerCheck(int clientId);

        int HowManyCarTheClient(int clientId);

        string PopularCar(int clientId);
    }
}
