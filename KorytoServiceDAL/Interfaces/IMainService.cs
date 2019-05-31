using KorytoServiceDAL.BindingModel;
using KorytoServiceDAL.ViewModel;
using System.Collections.Generic;

namespace KorytoServiceDAL.Interfaces
{
    public interface IMainService
    {
        List<OrderViewModel> GetList();

        List<OrderViewModel> GetClientOrders(int clientId);

        void CreateOrder(OrderBindingModel model);

        void TakeOrderInWork(OrderBindingModel model);

        void FinishOrder(OrderBindingModel model);

        void PayOrder(OrderBindingModel model);

        void ReserveOrder(OrderBindingModel model);
    }
}
