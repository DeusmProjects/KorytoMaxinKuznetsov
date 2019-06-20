using KorytoServiceDAL.BindingModel;
using KorytoServiceDAL.ViewModel;
using System.Collections.Generic;

namespace KorytoServiceDAL.Interfaces
{
    public interface IReportService
    {
        List<LoadRequestReportViewModel> GetDetailsRequest(ReportBindingModel model);

        List<LoadOrderReportViewModel> GetDetailsOrder(ReportBindingModel model);

        void SaveDetailsReport(List<LoadRequestReportViewModel> DetailsRequest, List<LoadOrderReportViewModel> DetailsOrder, string fileName, ReportBindingModel model);

        List<ClientOrdersViewModel> GetClientOrders(ReportBindingModel model, int clientId);

        void SaveClientOrders(ReportBindingModel model, int clientId);

        void SaveClientReserveWord(OrderViewModel model, string fileName);

        void SaveClientReserveExcel(OrderViewModel model, string fileName);
    }
}
