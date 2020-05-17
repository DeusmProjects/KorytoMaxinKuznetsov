using KorytoModel;
using KorytoServiceDAL.BindingModel;
using KorytoServiceDAL.ViewModel;
using System.Collections.Generic;

namespace KorytoServiceDAL.Interfaces
{
    public interface IMainService
    {
        List<OrderViewModel> GetList();

        List<OrderViewModel> GetClientOrders(int clientId);

        OrderViewModel GetElement(int id);

        void CreateOrder(OrderBindingModel model);

        void TakeOrderInWork(OrderBindingModel model);

        void FinishOrder(OrderBindingModel model);

        void PayOrder(OrderBindingModel model);

        void ReserveOrder(OrderBindingModel model);

        void SaveDataBaseClient();

        void SaveDataBaseAdmin();
    }
}
