using KorytoServiceDAL.BindingModel;
using KorytoServiceDAL.ViewModel;
using System.Collections.Generic;

namespace KorytoServiceDAL.Interfaces
{
    public interface IReportService
    {
        List<ClientOrdersViewModel> GetDetailReguest(ReportBindingModel model);

        void SaveDetailReguest(ReportBindingModel model);

        List<ClientOrdersViewModel> GetClientOrders(ReportBindingModel model);

        void SaveClientOrders(ReportBindingModel model);
    }
}
