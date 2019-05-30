using KorytoServiceDAL.BindingModel;
using KorytoServiceDAL.ViewModel;
using System.Collections.Generic;

namespace KorytoServiceDAL.Interfaces
{
    public interface ICarService
    {
        List<CarViewModel> GetList();

        List<CarViewModel> GetFilteredList();

        CarViewModel GetElement(int id);

        void AddElement(CarBindingModel model);

        void UpdateElement(CarBindingModel model);

        void DeleteElement(int id);
    }
}
