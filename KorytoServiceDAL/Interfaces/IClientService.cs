using KorytoServiceDAL.BindingModel;
using KorytoServiceDAL.ViewModel;
using System.Collections.Generic;

namespace KorytoServiceDAL.Interfaces
{
    public interface IClientService
    {
        List<ClientViewModel> GetList();

        ClientViewModel GetElement(int id);

        void AddElement(ClientBindingModel model);

        void UpdateElement(ClientBindingModel model);

        void DeleteElement(int id);
    }
}
