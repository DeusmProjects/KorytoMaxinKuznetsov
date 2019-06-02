namespace KorytoServiceDAL.Interfaces
{
    public interface IStatisticService
    {
        (string name, int count) GetMostPopularCar();

        decimal GetAverageCustomerCheck(int clientId);

        int GetClientCarsCount(int clientId);

        (string name, int count) GetPopularCarClient(int clientId);
    }
}
