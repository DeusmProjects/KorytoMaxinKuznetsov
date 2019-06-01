namespace KorytoServiceDAL.Interfaces
{
    public interface IStatisticService
    {
        (string name, int count) GetMostPopularCar();

        decimal AverageCustomerCheck(int clientId);

        int GetClientCarsCount(int clientId);

        string PopularCar(int clientId);
    }
}
