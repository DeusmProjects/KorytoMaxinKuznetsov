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
using Microsoft.Office.Interop.Excel;
using Global = Microsoft.Office.Interop.Excel.Global;

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

            var winword = new Microsoft.Office.Interop.Word.Application();

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

                int countRows = model.OrderCars.Count();

                foreach (var car in model.OrderCars)
                {
                    countRows += context.CarDetails.Where(rec => rec.CarId == car.CarId).Count();
                }

                table = document.Tables.Add(rangeTable, countRows, 4, ref
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

        public void SaveClientReserveExcel(OrderViewModel model, string fileName)
        {
            var excel = new Microsoft.Office.Interop.Excel.Application();

            try
            {
                if (File.Exists(fileName))
                {
                    excel.Workbooks.Open(fileName, Type.Missing, Type.Missing,
                   Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                   Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                   Type.Missing,
                    Type.Missing);
                }
                else
                {
                    excel.SheetsInNewWorkbook = 1;
                    excel.Workbooks.Add(Type.Missing);
                    excel.Workbooks[1].SaveAs(fileName, XlFileFormat.xlExcel8,
                    Type.Missing,
                     Type.Missing, false, false, XlSaveAsAccessMode.xlNoChange,
                    Type.Missing,
                     Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }
                Sheets excelsheets = excel.Workbooks[1].Worksheets;
                //Получаем ссылку на лист
                var excelworksheet = (Worksheet)excelsheets.get_Item(1);
                //очищаем ячейки
                excelworksheet.Cells.Clear();
                //настройки страницы
                excelworksheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                excelworksheet.PageSetup.CenterHorizontally = true;
                excelworksheet.PageSetup.CenterVertically = true;
                //получаем ссылку на первые 3 ячейки
                Microsoft.Office.Interop.Excel.Range excelcells =
               excelworksheet.get_Range("A1", "C1");
                //объединяем их
                excelcells.Merge(Type.Missing);
                //задаем текст, настройки шрифта и ячейки
                excelcells.Font.Bold = true;
                excelcells.Value2 = "Зарезервированный заказ";
                excelcells.RowHeight = 25;
                excelcells.HorizontalAlignment =
               Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                excelcells.VerticalAlignment =
           Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                excelcells.Font.Name = "Times New Roman";
                excelcells.Font.Size = 14;
                excelcells = excelworksheet.get_Range("A2", "C2");
                excelcells.Merge(Type.Missing);
                excelcells.Value2 = "от " + model.DateCreate;
                excelcells.RowHeight = 20;
                excelcells.HorizontalAlignment =
               Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                excelcells.VerticalAlignment =
               Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                excelcells.Font.Name = "Times New Roman";
                excelcells.Font.Size = 12;

                if (model != null)
                {
                    excelcells = excelworksheet.get_Range("C1", "C1");
                    foreach (var elem in model.OrderCars)
                    {
                        //спускаемся на 2 ячейку вниз и 2 ячейкт влево
                        excelcells = excelcells.get_Offset(2, -2);
                        excelcells.ColumnWidth = 15;
                        excelcells.Value2 = elem.CarName;
                        excelcells = excelcells.get_Offset(1, 1);

                        var carDetails = context.CarDetails
                            .Where(rec => rec.CarId == elem.CarId);

                        //обводим границы
                        if (carDetails.Count() > 0)
                        {
                            //получаем ячейкт для выбеления рамки под таблицу
                            var excelBorder =
                             excelworksheet.get_Range(excelcells,
                             excelcells.get_Offset(carDetails.Count() - 1, 1));
                            excelBorder.Borders.LineStyle =
                           Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                            excelBorder.Borders.Weight =
                           Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
                            excelBorder.HorizontalAlignment = Constants.xlCenter;
                            excelBorder.VerticalAlignment = Constants.xlCenter;
                            excelBorder.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,

                            Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium,

                            Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                             1);
                            foreach (var carDetail in carDetails)
                            {
                                excelcells.Value2 = context.Details.FirstOrDefault(detail => detail.Id == carDetail.DetailId).DetailName;
                                excelcells.ColumnWidth = 10;
                                excelcells.get_Offset(0, 1).Value2 = carDetail.Amount;
                                excelcells = excelcells.get_Offset(1, 0);
                            }
                        }

                        excelcells = excelcells.get_Offset(0, -1);
                        excelcells.Value2 = "Итого машин";
                        excelcells.Font.Bold = true;
                        excelcells = excelcells.get_Offset(0, 2);
                        excelcells.Value2 = elem.Amount;
                        excelcells.Font.Bold = true;

                    }
                }
                //сохраняем
                excel.Workbooks[1].Save();
                excel.Workbooks[1].Close();
                excel.Quit();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //закрываем
                excel.Quit();
            }
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


        public void SaveDetailsReport(List<LoadRequestReportViewModel> DetailsRequest, List<LoadOrderReportViewModel> DetailsOrder, string fileName, ReportBindingModel model)
        {
            if (!File.Exists("TIMCYR.TTF"))
            {
                File.WriteAllBytes("TIMCYR.TTF", Properties.Resources.TIMCYR);
            }

            Document doc = new Document(PageSize.A4.Rotate());
            BaseFont baseFont = BaseFont.CreateFont("TIMCYR.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            using (var writer = PdfWriter.GetInstance(doc, new FileStream(fileName, FileMode.Create)))
            {
                doc.Open();
                var title = new Phrase("Ревизия", new Font(baseFont, 16, Font.BOLD));
                Paragraph prTitle = new Paragraph(title)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 5
                };
                doc.Add(prTitle);

                var t1 = new Phrase("От " + model.DateFrom.ToString() + " по " + model.DateTo.ToString(), new Font(baseFont, 16, Font.BOLD));
                Paragraph prT1 = new Paragraph(t1)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 50
                };
                doc.Add(prT1);


                var phraseReqTitle = new Phrase("Пополнение деталей по заявкам :", new Font(baseFont, 16, Font.BOLD));
                Paragraph paragraphReq = new Paragraph(phraseReqTitle)
                {
                    Alignment = Element.ALIGN_LEFT,
                    SpacingAfter = 50
                };
                doc.Add(paragraphReq);

                foreach (var element in DetailsRequest)
                {
                    var phrase = new Phrase("Заявка : " + element.DateCreate, new Font(baseFont, 12, Font.BOLD));
                    Paragraph paragraph = new Paragraph(phrase)
                    {
                        Alignment = Element.ALIGN_LEFT,
                        SpacingAfter = 10
                    };
                    doc.Add(paragraph);
                    FillTable(element.Details, doc, baseFont, CreateHeadAndTable(doc, baseFont));
                }

                var phraseOrdTitle = new Phrase("Расходы деталей по заказаным автомобилям :", new Font(baseFont, 16, Font.BOLD));
                Paragraph paragraphOrd = new Paragraph(phraseOrdTitle)
                {
                    Alignment = Element.ALIGN_LEFT,
                    SpacingAfter = 50,
                    SpacingBefore = 50
                };
                doc.Add(paragraphOrd);

                foreach (var element in DetailsOrder)
                {
                    var p0 = new Phrase("Заказ номер №" + element.OrderId, new Font(baseFont, 12, Font.BOLD));
                    Paragraph pr0 = new Paragraph(p0)
                    {
                        Alignment = Element.ALIGN_LEFT,
                        SpacingAfter = 5

                    };
                    doc.Add(pr0);

                    var p1 = new Phrase("Заказ от : " + element.DateCreate, new Font(baseFont, 12, Font.BOLD));
                    Paragraph pr1 = new Paragraph(p1)
                    {
                        Alignment = Element.ALIGN_LEFT,
                        SpacingAfter = 5

                    };
                    doc.Add(pr1);

                    var p2 = new Phrase("Авто : " + element.CarName, new Font(baseFont, 12, Font.BOLD));
                    Paragraph pr2 = new Paragraph(p2)
                    {
                        Alignment = Element.ALIGN_LEFT,
                        SpacingAfter = 5
                    };
                    doc.Add(pr2);

                    var p3 = new Phrase("Количество авто : " + element.CarAmount, new Font(baseFont, 12, Font.BOLD));
                    Paragraph pr3 = new Paragraph(p3)
                    {
                        Alignment = Element.ALIGN_LEFT,
                        SpacingAfter = 5
                    };
                    doc.Add(pr3);

                    FillTable(element.Details, doc, baseFont, CreateHeadAndTable(doc, baseFont), element.CarAmount);
                }

                doc.Close();
            }
        }

        private PdfPTable CreateHeadAndTable(Document doc, BaseFont baseFont)
        {
            PdfPTable table = new PdfPTable(3);

            table.AddCell(new PdfPCell(new Phrase("Деталь", new Font(baseFont, 12, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            table.AddCell(new PdfPCell(new Phrase("Количество", new Font(baseFont, 12, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            table.AddCell(new PdfPCell(new Phrase("Итог", new Font(baseFont, 12, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            Paragraph p = new Paragraph();
            table.HorizontalAlignment = Element.ALIGN_LEFT;
            table.SpacingBefore = 5;
            table.SpacingAfter = 25;
            p.Add(table);

            return table;
        }

        private PdfPTable FillTable(IEnumerable<Tuple<string, int>> Details, Document doc, BaseFont baseFont, PdfPTable table)
        {
            PdfPTable newtable = table;

            foreach (var element in Details)
            {
                newtable.AddCell(new PdfPCell(new Phrase(element.Item1, new Font(baseFont, 12, Font.NORMAL)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                });

                newtable.AddCell(new PdfPCell(new Phrase(element.Item2.ToString(), new Font(baseFont, 12, Font.NORMAL)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
                newtable.AddCell(new PdfPCell(new Phrase(element.Item2.ToString(), new Font(baseFont, 12, Font.NORMAL)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
            }

            doc.Add(newtable);
            return newtable;
        }

        private PdfPTable FillTable(List<CarDetailViewModel> DetailsOrder, Document doc, BaseFont baseFont, PdfPTable table, int carAmount)
        {

            PdfPTable newtable = table;

            foreach (var element in DetailsOrder)
            {
                newtable.AddCell(new PdfPCell(new Phrase(element.DetailName, new Font(baseFont, 12, Font.NORMAL)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                });

                newtable.AddCell(new PdfPCell(new Phrase(element.Amount.ToString(), new Font(baseFont, 12, Font.NORMAL)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                });

                int res = element.Amount * carAmount;
                newtable.AddCell(new PdfPCell(new Phrase(res.ToString(), new Font(baseFont, 12, Font.NORMAL)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
            }

            doc.Add(newtable);
            return table;
        }

        public List<LoadRequestReportViewModel> GetDetailsRequest(ReportBindingModel model)
        {
            List<LoadRequestReportViewModel> DetailsRequest = context.Requests
                .Where(requests => requests.DateCreate >= model.DateFrom && requests.DateCreate <= model.DateTo)
                .ToList()
                .Select(selectRequest => new LoadRequestReportViewModel
                {
                    DateCreate = selectRequest.DateCreate.ToString(),
                    Details = context.DetailRequests
                        .Where(details => details.RequestId == selectRequest.Id)
                        .ToList()
                        .Select(selectDetail => new Tuple<string, int>(
                            context.Details
                            .FirstOrDefault(detail => detail.Id == selectDetail.DetailId).DetailName, selectDetail.Amount))
                        .ToList()
                })
            .ToList();

            return DetailsRequest;
        }

        public List<LoadOrderReportViewModel> GetDetailsOrder(ReportBindingModel model)
        {
            List<LoadOrderReportViewModel> cars = context.Cars
                .Select(rec => new LoadOrderReportViewModel
                {
                    CarName = rec.CarName,
                    CarId = rec.Id,
                    Details = context.CarDetails.Where(r => r.CarId == rec.Id).ToList().Select(rs => new CarDetailViewModel
                    {
                        CarId = rs.CarId,
                        Amount = rs.Amount,
                        DetailId = rs.DetailId,
                        DetailName = context.Details.FirstOrDefault(d => d.Id == rs.DetailId).DetailName

                    }).ToList()
                })
           .ToList();

            List<LoadOrderReportViewModel> orders = context.Orders
                .Where(rec => rec.DateImplement >= model.DateFrom && rec.DateImplement <= model.DateTo)
                .Select(rec => new LoadOrderReportViewModel
                {
                    DateCreate = rec.DateImplement.ToString(),
                    OrderId = rec.Id
                })
                .ToList();

            List<OrderCar> orderCars = context.OrderCars.ToList();

            List<LoadOrderReportViewModel> Details = new List<LoadOrderReportViewModel>();

            foreach (var element in orderCars)
            {
                LoadOrderReportViewModel save = new LoadOrderReportViewModel();

                foreach (var order in orders)
                {
                    if (element.OrderId == order.OrderId)
                    {
                        save.DateCreate = order.DateCreate;
                        save.OrderId = order.OrderId;
                        break;
                    }
                }

                foreach (var car in cars)
                {
                    if (element.CarId == car.CarId && save.OrderId == element.OrderId)
                    {
                        save.CarName = car.CarName;
                        save.CarAmount = element.Amount;
                        save.Details = car.Details;
                        break;
                    }
                }
                Details.Add(save);
            }

            return Details;
        }

        private StatisticViewModel GetStatistic(int clientId)
        {
            var statisticService = new StatisticServiceDB(context);

            return new StatisticViewModel
            {
                AverageCheck = statisticService.GetAverageCheck(),
                AverageCheckClient = statisticService.GetAverageCustomerCheck(clientId),
                CountCarsClient = statisticService.GetClientCarsCount(clientId),
                MostPopularCar = statisticService.GetMostPopularCar(),
                MostPopularCarClient = statisticService.GetPopularCarClient(clientId)
            };
        }

        public void PrintStatistic(int clientId, string fileName)
        {
            var model = GetStatistic(clientId);

            if (!File.Exists("TIMCYR.TTF"))
            {
                File.WriteAllBytes("TIMCYR.TTF", Properties.Resources.TIMCYR);
            }

            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);

            var document = new Document();

            PdfWriter writer = PdfWriter.GetInstance(document, fs);

            baseFont = BaseFont.CreateFont("TIMCYR.TTF", BaseFont.IDENTITY_H,
                BaseFont.NOT_EMBEDDED);

            document.Open();

            PrintHeader("Общая статистика", document);

            var table = new PdfPTable(2);
            var cell = new PdfPCell
            {
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_CENTER
            };
            table.AddCell(cell);
            table.SetTotalWidth(new float[] { 160, 140 });

            var fontForCellBold = new Font(baseFont, 10, Font.BOLD);
            var fontForCell = new Font(baseFont, 10);

            table.AddCell(new PdfPCell(new Phrase("Самая популярная машина", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            table.AddCell(new PdfPCell(new Phrase("Средний чек", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            table.AddCell(new PdfPCell(new Phrase($"{model.MostPopularCar.Item1} ( {model.MostPopularCar.Item2} штук)", fontForCell))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            table.AddCell(new PdfPCell(new Phrase(model.AverageCheck.ToString(CultureInfo.InvariantCulture), fontForCell))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            document.Add(table);

            PrintHeader("Статистика клиента", document);

            table = new PdfPTable(3);
            cell = new PdfPCell
            {
                Colspan = 3,
                HorizontalAlignment = Element.ALIGN_CENTER
            };
            table.AddCell(cell);
            table.SetTotalWidth(new float[] { 160, 160, 140 });

            table.AddCell(new PdfPCell(new Phrase("Всего машин куплено", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            table.AddCell(new PdfPCell(new Phrase("Любимая машина", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            table.AddCell(new PdfPCell(new Phrase("Средний чек клиента", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            table.AddCell(new PdfPCell(new Phrase(model.CountCarsClient.ToString(), fontForCell))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            table.AddCell(new PdfPCell(new Phrase($"{model.MostPopularCarClient.Item1} ( {model.MostPopularCarClient.Item2} штук)", fontForCell))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            table.AddCell(new PdfPCell(new Phrase(model.AverageCheckClient.ToString(CultureInfo.InvariantCulture), fontForCell))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            document.Add(table);

            var diagram = Image.GetInstance("D:\\reports\\diagram.png");
            diagram.Alignment = Element.ALIGN_CENTER;
            document.Add(diagram);

            document.Close();
            fs.Close();
        }
    }
}


