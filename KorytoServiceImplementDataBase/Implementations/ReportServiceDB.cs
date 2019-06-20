using iTextSharp.text;
using iTextSharp.text.pdf;
using KorytoModel;
using KorytoServiceDAL.BindingModel;
using KorytoServiceDAL.Interfaces;
using KorytoServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Office.Interop.Word;
using Document = iTextSharp.text.Document;
using Font = iTextSharp.text.Font;
using Paragraph = iTextSharp.text.Paragraph;

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

        public void SaveClientReserveWord(OrderViewModel model, string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            var winword = new Application();

            try
            {
                object missing = System.Reflection.Missing.Value;

                var document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);

                var paragraph = document.Paragraphs.Add(missing);
                var range = paragraph.Range;

                range.Text = "Зарезервированный заказ";

                var font = range.Font;
                font.Size = 16;
                font.Name = "Times New Roman";
                font.Bold = 1;

                var paragraphFormat = range.ParagraphFormat;
                paragraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                paragraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
                paragraphFormat.SpaceAfter = 10;
                paragraphFormat.SpaceBefore = 0;

                range.InsertParagraphAfter();

                var paragraphTable = document.Paragraphs.Add(Type.Missing);
                var rangeTable = paragraphTable.Range;
                var table = document.Tables.Add(rangeTable, 2, 3, ref
                    missing, ref missing);
                font = table.Range.Font;
                font.Size = 14;
                font.Name = "Times New Roman";
                var paragraphTableFormat = table.Range.ParagraphFormat;
                paragraphTableFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
                paragraphTableFormat.SpaceAfter = 0;
                paragraphTableFormat.SpaceBefore = 0;

                table.Cell(1, 1).Range.Text = "ФИО клиента";
                table.Cell(1, 2).Range.Text = "Сумма заказа";
                table.Cell(1, 3).Range.Text = "Дата резервации";

                table.Cell(2, 1).Range.Text = model.ClientFIO;
                table.Cell(2, 2).Range.Text = model.TotalSum.ToString();
                table.Cell(2, 3).Range.Text = model.DateCreate;

                table.Borders.InsideLineStyle = WdLineStyle.wdLineStyleInset;
                table.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;

                range.InsertParagraphAfter();

                range = paragraph.Range;
                range.Text = "Машины в заказе";

                font = range.Font;
                font.Size = 14;
                font.Name = "Times New Roman";
                font.Bold = 1;

                paragraphFormat = range.ParagraphFormat;
                paragraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                paragraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
                paragraphFormat.SpaceAfter = 10;
                paragraphFormat.SpaceBefore = 0;

                range.InsertParagraphAfter();

                paragraphTable = document.Paragraphs.Add(Type.Missing);
                rangeTable = paragraphTable.Range;

                table = document.Tables.Add(rangeTable, 20, 4, ref
                    missing, ref missing);
                font = table.Range.Font;
                font.Size = 14;
                font.Name = "Times New Roman";
                font.Bold = 0;
                paragraphTableFormat = table.Range.ParagraphFormat;
                paragraphTableFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
                paragraphTableFormat.SpaceAfter = 0;
                paragraphTableFormat.SpaceBefore = 0;

                table.Cell(1, 1).Range.Text = "Название";
                table.Cell(1, 2).Range.Text = "Количество";
                table.Cell(1, 3).Range.Text = "Комплектация";
                table.Cell(1, 4).Range.Text = "Количество";

                int curRow = 2;
                int indexCar = 0;

                while (indexCar < model.OrderCars.Count)
                {
                    table.Cell(curRow, 1).Range.Text = model.OrderCars[indexCar].CarName;
                    table.Cell(curRow, 2).Range.Text = model.OrderCars[indexCar].Amount.ToString();

                    int carId = model.OrderCars[indexCar].CarId;

                    var carDetails = context.CarDetails
                        .Where(rec => rec.CarId == carId)
                        .Select(rec => new CarDetailViewModel
                        {
                            DetailName = context.Details.FirstOrDefault(det => det.Id == rec.DetailId).DetailName,
                            Amount = rec.Amount
                        }).ToArray();

                    int indexDet = 0;

                    while (indexDet < carDetails.Length)
                    {
                        table.Cell(curRow, 3).Range.Text = carDetails[indexDet].DetailName;
                        table.Cell(curRow, 4).Range.Text = carDetails[indexDet].Amount.ToString();
                        indexDet++;
                        curRow++;
                    }

                    indexCar++;
                    curRow++;
                }

                table.Borders.InsideLineStyle = WdLineStyle.wdLineStyleInset;
                table.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;

                paragraph = document.Paragraphs.Add(missing);
                range = paragraph.Range;
                range.Text = "Дата: " + DateTime.Now.ToLongDateString();
                font = range.Font;
                font.Size = 12;
                font.Name = "Times New Roman";
                paragraphFormat = range.ParagraphFormat;
                paragraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                paragraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
                paragraphFormat.SpaceAfter = 10;
                paragraphFormat.SpaceBefore = 10;

                range.InsertParagraphAfter();
                //сохраняем
                object fileFormat = WdSaveFormat.wdFormatXMLDocument;
                document.SaveAs(fileName, ref fileFormat, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing);
                document.Close(ref missing, ref missing, ref missing);

                var client = context.Clients.FirstOrDefault(rec => rec.Id == model.ClientId);

                MailService.SendEmail(client?.Mail, "Оповещение по заказам",
                    $"Заказ №{model.Id} от {model.DateCreate} зарезервирован успешно", fileName);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                winword.Quit();
            }
        }

        public void SaveClientReserveExel(OrderViewModel model, string fileName)
        {

        }

        public List<ClientOrdersViewModel> GetClientOrders(ReportBindingModel model, int clientId)
        {
            return context.Orders
                .Where(rec => rec.DateCreate >= model.DateFrom &&
                              rec.DateCreate <= model.DateTo && 
                              rec.ClientId == clientId)
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

        public void SaveClientOrders(ReportBindingModel model, int clientId)
        {

            if (!File.Exists("TIMCYR.TTF"))
            {
                File.WriteAllBytes("TIMCYR.TTF", Properties.Resources.TIMCYR);
            }

            FileStream fs = new FileStream(model.FileName, FileMode.OpenOrCreate, FileAccess.Write);

            var document = new Document();

            PdfWriter writer = PdfWriter.GetInstance(document, fs);

            baseFont = BaseFont.CreateFont("TIMCYR.TTF", BaseFont.IDENTITY_H,
                BaseFont.NOT_EMBEDDED);

            document.Open();

            PrintHeader($"Заказы клиента за период с {string.Format("{0:dd.MM.yyyy}", model.DateFrom)} по {string.Format("{0:dd.MM.yyyy}", model.DateTo)}", document);

            var cb = writer.DirectContent;

            var clientOrders = GetClientOrders(model, clientId);

            foreach (var order in clientOrders)
            {
                PrintHeader("Информация по заказу", document);
                PrintOrderInfo(order, document);
                DrawLine(cb, document, writer);
            }

            document.Close();
            fs.Close();
        }

        private void DrawLine(PdfContentByte cb, Document doc, PdfWriter writer)
        {
            cb.MoveTo(0, writer.GetVerticalPosition(true) - 20);
            cb.LineTo(doc.PageSize.Width, writer.GetVerticalPosition(true) - 20);
            cb.Stroke();
            doc.Add(Chunk.NEWLINE);
        }

        private void PrintHeader(string text, Document doc)
        {
            var phraseTitle = new Phrase(text, new Font(baseFont, 16, Font.BOLD));
            Paragraph paragraph = new Paragraph(phraseTitle)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 12
            };
            doc.Add(paragraph);
        }

        private void PrintOrderInfo(ClientOrdersViewModel clientOrder, Document doc)
        {
            var table = new PdfPTable(4);
            var cell = new PdfPCell
            {
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_CENTER //0=Left, 1=Centre, 2=Right
            };
            table.AddCell(cell);
            table.SetTotalWidth(new float[] { 160, 140, 160, 100 });

            var fontForCellBold = new Font(baseFont, 10, Font.BOLD);

            table.AddCell(new PdfPCell(new Phrase("ФИО клиента", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            table.AddCell(new PdfPCell(new Phrase("Сумма заказа", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            table.AddCell(new PdfPCell(new Phrase("Дата заказа", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            table.AddCell(new PdfPCell(new Phrase("Статус", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            var fontForCells = new Font(baseFont, 10);

            table.AddCell(new PdfPCell(new Phrase(clientOrder.ClientName, fontForCells))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            table.AddCell(new PdfPCell(new Phrase(clientOrder.TotalSum.ToString(CultureInfo.InvariantCulture), fontForCells))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            table.AddCell(new PdfPCell(new Phrase(clientOrder.DateCreateOrder, fontForCells))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            table.AddCell(new PdfPCell(new Phrase(clientOrder.StatusOrder, fontForCells))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            doc.Add(table);

            PrintHeader("Машины в заказе", doc);

            var tableCar = new PdfPTable(3);
            var cellCar = new PdfPCell { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER };
            tableCar.AddCell(cellCar);
            tableCar.SetTotalWidth(new float[] { 60, 40, 180 });

            tableCar.AddCell(new PdfPCell(new Phrase("Название", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            tableCar.AddCell(new PdfPCell(new Phrase("Количество", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            tableCar.AddCell(new PdfPCell(new Phrase("Комплектации", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            foreach (var car in clientOrder.OrderCars)
            {
                PrintOrderCars(car, doc, tableCar);
            }

            doc.Add(tableCar);
        }

        private void PrintOrderCars(OrderCarViewModel orderCar, Document doc, PdfPTable table)
        {
            var fontForCells = new Font(baseFont, 10);
            var fontForCellBold = new Font(baseFont, 10, Font.BOLD);

            table.AddCell(new PdfPCell(new Phrase(orderCar.CarName, fontForCells))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            table.AddCell(new PdfPCell(new Phrase(orderCar.Amount.ToString(), fontForCells))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            var carDetails = context.CarDetails
                .Where(rec => rec.CarId == orderCar.CarId)
                .Select(rec => new CarDetailViewModel
                {
                    Amount = rec.Amount,
                    DetailName = rec.Detail.DetailName,
                    DetailId = rec.DetailId
                }).ToList();

            var tableDetail = new PdfPTable(2);
            var cellDet = new PdfPCell { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER };
            tableDetail.AddCell(cellDet);
            tableDetail.SetTotalWidth(new float[] { 60, 40 });


            tableDetail.AddCell(new PdfPCell(new Phrase("Название", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            tableDetail.AddCell(new PdfPCell(new Phrase("Количество", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            foreach (var carDetail in carDetails)
            {
                PrintCarDetails(carDetail, tableDetail);
            }

            table.AddCell(tableDetail);
        }

        private void PrintCarDetails(CarDetailViewModel carDetail, PdfPTable table)
        {
            var fontForCell = new Font(baseFont, 10);

            table.AddCell(new PdfPCell(new Phrase(carDetail.DetailName, fontForCell))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            table.AddCell(new PdfPCell(new Phrase(carDetail.Amount.ToString(), fontForCell))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
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

