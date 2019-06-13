using KorytoServiceDAL.BindingModel;
using KorytoServiceDAL.ViewModel;
using System.Collections.Generic;

namespace KorytoServiceDAL.Interfaces
{
    public interface IReportService
    {
        List<RequestLoadViewModel> GetDetailReguest(ReportBindingModel model);

        void SaveLoadRequest(List<RequestLoadViewModel> list, string fileName);

        List<ClientOrdersViewModel> GetClientOrders(ReportBindingModel model);

        void SaveClientOrders(ReportBindingModel model);
    }
}
