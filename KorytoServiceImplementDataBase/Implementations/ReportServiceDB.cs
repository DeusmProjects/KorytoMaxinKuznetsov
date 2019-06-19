using iTextSharp.text;
using iTextSharp.text.pdf;
using KorytoModel;
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
using System.Xml.Linq;

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

        public List<RequestLoadViewModel> GetDetailReguest(ReportBindingModel model)
        {
            var requests = context.Requests
                .Where(rec => rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo).ToList();

            for (int request = 0; request < requests.Count(); request++)
            {
                requests[request].DetailRequests = new List<DetailRequest>();
            }

            var detailsRequest = context.DetailRequests.ToList();

            var details = context.Details.ToList();

            for (int i = 0; i < detailsRequest.Count(); i++)
            {
                int detailId = detailsRequest[i].DetailId;

                for (int j = 0; j < details.Count(); j++)
                {
                    if (details[j].Id == detailId)
                    {
                        detailsRequest[i].Detail = details[j];
                        break;
                    }
                }
            }

            var result = new List<RequestLoadViewModel>();

            for (int i = 0; i < requests.Count(); i++)
            {
                result.Add(new RequestLoadViewModel
                {
                    DateRequst = requests[i].DateCreate.ToString(),
                    Details = requests[i].DetailRequests.Select(r => new Tuple<string, int>(r.Detail.DetailName, r.Amount))
                });
            }

            return result;
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

        public void SaveLoadRequest(List<RequestLoadViewModel> list, string fileName)
        {

            Document doc = new Document(PageSize.A4.Rotate());

            using (var writer = PdfWriter.GetInstance(doc, new FileStream(fileName, FileMode.Create)))
            {
                doc.Open();
                PdfPTable table = CreateTable();
                table = FillTabel(list, table);
                doc.Add(table);
                doc.Close();
            }
        }

        private PdfPTable FillTabel(List<RequestLoadViewModel> list, PdfPTable table)
        {
            PdfPTable Table = table;

            int rows = CountRows(list);

            PdfPCell cell = new PdfPCell();

            BaseFont baseFont = BaseFont.CreateFont("TIMCYR.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            var fontForCells = new Font(baseFont, 10, Font.NORMAL);

            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (j == 0)
                    {
                        cell = new PdfPCell(new PdfPCell(new Phrase("Request (date) : "))
                        {
                            HorizontalAlignment = Element.ALIGN_CENTER
                        });
                    }
                    else
                    {
                        cell = new PdfPCell(new PdfPCell(new Phrase(""))
                        {
                            HorizontalAlignment = Element.ALIGN_CENTER
                        });
                    }
                    Table.AddCell(cell);
                }

                for (int j = 0; j < list[i].Details.Count(); j++)
                {
                    RequestLoadViewModel reportElement = list[i];

                    for (int k = 0; k < 3; k++)
                    {
                        if (j == 0 && k == 0)
                        {
                            string date = reportElement.DateRequst;
                            cell = new PdfPCell(new PdfPCell(new Phrase(date))
                            {
                                HorizontalAlignment = Element.ALIGN_CENTER
                            });

                        }
                        if (j != 0 && k == 0)
                        {
                            cell = new PdfPCell(new PdfPCell(new Phrase(""))
                            {
                                HorizontalAlignment = Element.ALIGN_CENTER
                            });

                        }
                        if (k == 1)
                        {
                            string name = reportElement.Details.ElementAt(j).Item1;
                            cell = new PdfPCell(new PdfPCell(new Phrase(name))
                            {
                                HorizontalAlignment = Element.ALIGN_CENTER
                            });

                        }
                        if (k == 2)
                        {
                            string amount = reportElement.Details.ElementAt(j).Item2.ToString();
                            cell = new PdfPCell(new PdfPCell(new Phrase(amount))
                            {
                                HorizontalAlignment = Element.ALIGN_CENTER
                            });

                        }
                        Table.AddCell(cell);
                    }
                }
            }

            return Table;
        }

        private int CountRows(List<RequestLoadViewModel> list)
        {
            int rows = 0;
            for (int i = 0; i < list.Count; i++)
            {
                rows += 2;
                for (int j = 0; j < list[i].Details.Count(); j++)
                {
                    rows++;
                }
            }
            return rows;
        }

        private PdfPTable CreateTable()
        {
            if (!File.Exists("TIMCYR.TTF"))
            {
                File.WriteAllBytes("TIMCYR.TTF", Properties.Resources.TIMCYR);
            }

            BaseFont baseFont = BaseFont.CreateFont("TIMCYR.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            var fontForCellBold = new Font(baseFont, 10, Font.BOLD);

            PdfPTable table = new PdfPTable(3);
            table.AddCell(new PdfPCell(new Phrase("Отчет", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            table.AddCell(new PdfPCell(new Phrase("Деталь", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            table.AddCell(new PdfPCell(new Phrase("Количество", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            return table;
        }

    }
}

