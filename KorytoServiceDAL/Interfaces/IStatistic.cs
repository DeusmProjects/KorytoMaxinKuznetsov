namespace KorytoServiceDAL.Interfaces
{
    public interface IStatistic
    {
        (string, int) GetMostPopularCar();

        decimal AverageCustomerCheck(int clientId);

        int HowManyCarTheClient(int clientId);

        string PopularCar(int clientId);
    }
}
