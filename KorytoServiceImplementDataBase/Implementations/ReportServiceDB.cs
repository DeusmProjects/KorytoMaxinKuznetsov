using iTextSharp.text;
using iTextSharp.text.pdf;
using KorytoServiceDAL.BindingModel;
using KorytoServiceDAL.Interfaces;
using KorytoServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Globalization;
using System.IO;
using System.Linq;

namespace KorytoServiceImplementDataBase.Implementations
{
    public class ReportServiceDB : IReportService
    {
        private readonly KorytoDbContext context;

        private static BaseFont baseFont;

        public ReportServiceDB(KorytoDbContext context)
        {
            this.context = context;
        }

        public List<ClientOrdersViewModel> GetClientOrders(ReportBindingModel model)
        {
            return context.Orders.Include(rec => rec.Client)
                //.Include(rec => rec.OrderCars)
                .Where(rec => rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo)
                .Select(rec => new ClientOrdersViewModel
                {
                    ClientName = rec.Client.ClientFIO,
                    DateCreateOrder =
                        SqlFunctions.DateName("dd", rec.DateCreate) + " " +
                        SqlFunctions.DateName("mm", rec.DateCreate) + " " +
                        SqlFunctions.DateName("yyyy", rec.DateCreate),
                    TotalSum = rec.TotalSum,
                    StatusOrder = rec.OrderStatus.ToString(),
                    OrderCars = context.OrderCars.Where(recPC => recPC.OrderId == rec.Id)
                        .Select(recPC => new OrderCarViewModel
                        {
                            Id = recPC.Id,
                            CarId = recPC.CarId,
                            OrderId = recPC.OrderId,
                            CarName = recPC.Car.CarName,
                            Amount = recPC.Amount
                        })
                        .ToList()
                })
                .ToList();
        }

        public List<ClientOrdersViewModel> GetDetailReguest(ReportBindingModel model)
        {
            throw new NotImplementedException();
        }

        public void SaveClientOrders(ReportBindingModel model)
        {
            var document = new Document();

            using (var writer = PdfWriter.GetInstance(document, new FileStream(model.FileName, FileMode.Create)))
            {
                document.Open();

                PrintHeader("Заказы клиента", document);

                var clientOrders = GetClientOrders(model);

                foreach (var order in clientOrders)
                {
                    PrintOrderInfo(order, document);
                }
            }
        }

        private static void PrintHeader(string text, Document doc)
        {
            var phraseTitle = new Phrase(text, new Font(baseFont, 16, Font.BOLD));
            Paragraph paragraph = new Paragraph(phraseTitle)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 12
            };
            doc.Add(paragraph);
        }

        private static void PrintOrderInfo(ClientOrdersViewModel clientOrder, Document doc)
        {

        }

        private static void PrintOrderCars(OrderCarViewModel orderCars, Document doc)
        {

        }

        private void PrintCarDetails(CarDetailViewModel carDetails, Document doc)
        {

        }

        public void SaveDetailReguest(ReportBindingModel model)
        {
            throw new NotImplementedException();
        }
    }
}
