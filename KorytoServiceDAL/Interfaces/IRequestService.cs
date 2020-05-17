using KorytoServiceDAL.BindingModel;
using KorytoServiceDAL.ViewModel;
using System.Collections.Generic;

namespace KorytoServiceDAL.Interfaces
{
    public interface IRequestService
    {
        List<RequestViewModel> GetList();

        RequestViewModel GetElement(int id);

        void AddElement(RequestBindingModel model);
      
        void DeleteElement(int id);

        LoadRequestReportViewModel GetDetailsRequest(int id);

        void SaveRequestToWord(LoadRequestReportViewModel request, string fileName);

        void SaveRequestToExcel(LoadRequestReportViewModel request, string fileName);
    }
}
