using iTextSharp.text;
using iTextSharp.text.pdf;
using KorytoServiceDAL.BindingModel;
using KorytoServiceDAL.Interfaces;
using KorytoServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.util;

namespace KorytoServiceImplementDataBase.Implementations
{
    public class ReportServiceDB : IReportService
    {
        private KorytoDbContext context;

        public ReportServiceDB(KorytoDbContext context)
        {
            this.context = context;
        }

        public List<ClientOrdersViewModel> GetClientOrders(ReportBindingModel model)
        {
            return context.Orders.Include(rec => rec.Client).Include(rec => rec.OrderCars)
            .Where(rec => rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo)
             .Select(rec => new ClientOrdersViewModel
             {
                 ClientName = rec.Client.ClientFIO,
                 DateCreateOrder = SqlFunctions.DateName("dd", rec.DateCreate)
                    + " " +
                     SqlFunctions.DateName("mm", rec.DateCreate) +
                    " " +
                     SqlFunctions.DateName("yyyy", rec.DateCreate),
                 TotalSum = rec.TotalSum,
                 StatusOrder = rec.OrderStatus.ToString()
             })
            .ToList();
        }

        public List<ClientOrdersViewModel> GetDetailReguest(ReportBindingModel model)
        {
            throw new NotImplementedException();
        }

        public void SaveClientOrders(ReportBindingModel model)
        {
            if (!File.Exists("TIMCYR.TTF"))
            {
                File.WriteAllBytes("TIMCYR.TTF", Properties.Resources.TIMCYR);
            }
            //открываем файл для работы
            FileStream fs = new FileStream(model.FileName, FileMode.OpenOrCreate,
           FileAccess.Write);
            //создаем документ, задаем границы, связываем документ и поток
            Document doc = new Document();
            doc.SetMargins(0.5f, 0.5f, 0.5f, 0.5f);
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);
            doc.Open();
            BaseFont baseFont = BaseFont.CreateFont("TIMCYR.TTF", BaseFont.IDENTITY_H,
           BaseFont.NOT_EMBEDDED);
            //вставляем заголовок
            var phraseTitle = new Phrase("Заказы клиентов", new Font(baseFont, 16, Font.BOLD));
            Paragraph paragraph = new Paragraph(phraseTitle)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 12
            };
            doc.Add(paragraph);
            var phrasePeriod = new Phrase("c " + model.DateFrom.Value.ToShortDateString()
           +
            " по " + model.DateTo.Value.ToShortDateString(),
           new Font(baseFont, 14,
           Font.BOLD));
            paragraph = new Paragraph(phrasePeriod)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 12
            };
            doc.Add(paragraph);
            //вставляем таблицу, задаем количество столбцов, и ширину колонок
            PdfPTable table = new PdfPTable(6)
            {
                TotalWidth = 800F
            };
            table.SetTotalWidth(new float[] { 160, 140, 160, 100, 100, 140 });
            //вставляем шапку
            PdfPCell cell = new PdfPCell();
            var fontForCellBold = new Font(baseFont, 10, Font.BOLD);
            table.AddCell(new PdfPCell(new Phrase("ФИО клиента", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Дата создания", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            table.AddCell(new PdfPCell(new Phrase("Изделие", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Количество", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            table.AddCell(new PdfPCell(new Phrase("Сумма", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Статус", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            //заполняем таблицу
            var list = GetClientOrders(model);
            var fontForCells = new Font(baseFont, 10);
            for (int i = 0; i < list.Count; i++)
            {
                cell = new PdfPCell(new Phrase(list[i].ClientName, fontForCells));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(list[i].DateCreateOrder, fontForCells));
                table.AddCell(cell);
                foreach(var car in list[i].OrderCars)
                {
                    cell = new PdfPCell(new Phrase(car.CarName.ToString(), fontForCells));
                    table.AddCell(cell);
                }
                cell = new PdfPCell(new Phrase(list[i].OrderCars.Count.ToString(), fontForCells));
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(list[i].TotalSum.ToString(), fontForCells));
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(list[i].StatusOrder, fontForCells));
                table.AddCell(cell);
            }
            //вставляем итого
            cell = new PdfPCell(new Phrase("Итого:", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Colspan = 4,
                Border = 0
            };
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(list.Sum(rec => rec.TotalSum).ToString(), fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Border = 0
            };
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("", fontForCellBold))
            {
                Border = 0
            };
            table.AddCell(cell);
            //вставляем таблицу
            doc.Add(table);
            doc.Close();
        }

        public void SaveDetailReguest(ReportBindingModel model)
        {
            throw new NotImplementedException();
        }
    }
}
