using KorytoModel;
using KorytoServiceDAL.BindingModel;
using KorytoServiceDAL.Interfaces;
using KorytoServiceDAL.ViewModel;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KorytoServiceImplementDataBase.Implementations
{
    public class RequestServiceDB : IRequestService
    {
        readonly KorytoDbContext context;

        public RequestServiceDB(KorytoDbContext context)
        {
            this.context = context;
        }

        public void AddElement(RequestBindingModel model)
        {

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {

                    var request = new Request
                    {
                        DateCreate = model.DateCreate
                    };

                    context.Requests.Add(request);
                    context.SaveChanges();

                    var groupDetails = model.DetailRequests
                        .GroupBy(record => record.DetailId)
                        .Select(record => new {detailId = record.Key, amount = record.Sum(r => r.Amount)});

                    foreach (var gr in groupDetails)
                    {
                        var detailRequest = new DetailRequest
                        {
                            RequestId = request.Id,
                            DetailId = gr.detailId,
                            Amount = gr.amount
                        };

                        context.DetailRequests.Add(detailRequest);
                        context.SaveChanges();

                        var updateDetail = context.Details.FirstOrDefault(record => record.Id == detailRequest.DetailId);

                        if (updateDetail == null) continue;
                        updateDetail.TotalAmount += detailRequest.Amount;
                        context.SaveChanges();
                    }

                    transaction.Commit();
                }

                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }

            }
        }

        public void DeleteElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var request = context.Requests.FirstOrDefault(record => record.Id == id);

                    if (request != null)
                    {
                        context.DetailRequests.RemoveRange(context.DetailRequests.Where(rec => rec.RequestId == id));

                        context.Requests.Remove(request);

                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Заявка не найдена");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public RequestViewModel GetElement(int id)
        {
            var request = context.Requests.FirstOrDefault(
                record => record.Id == id);

            if (request != null)
            {
                return new RequestViewModel
                {
                    Id = request.Id,
                    DateCreate = request.DateCreate,
                    DetailRequests = context.DetailRequests
                        .Where(record => record.RequestId == request.Id)
                        .Select(recPC => new DetailRequestViewModel
                        {
                            Id = recPC.Id,
                            DetailId = recPC.DetailId,
                            RequestId = recPC.RequestId,
                            Amount = recPC.Amount
                        })
                        .ToList()
                };

            }
            throw new Exception("Заявка не найдена");
        }

        public List<RequestViewModel> GetList()
        {
            var result = context.Requests.Select(record => new RequestViewModel
                {
                    Id = record.Id,
                    DateCreate = record.DateCreate,
                    DetailRequests = context.DetailRequests
                        .Where(r => r.RequestId == record.Id)
                        .Select(r => new DetailRequestViewModel
                        {
                            Id = r.Id,
                            DetailId = r.DetailId,
                            RequestId = r.RequestId,
                            Amount = r.Amount
                        })
                        .ToList()
                })
                .ToList();

            return result;
        }

        public LoadRequestReportViewModel GetDetailsRequest(int id)
        {
            var request = context.Requests.FirstOrDefault(
                record => record.Id == id);

            if (request != null)
            {
                LoadRequestReportViewModel report = new LoadRequestReportViewModel
                {

                    DateCreate = request.DateCreate.ToString(),

                    Details = context.DetailRequests
                        .Where(details => details.RequestId == request.Id)
                        .ToList()
                        .Select(selectDetail => new Tuple<string, int>(
                            context.Details
                            .FirstOrDefault(detail => detail.Id == selectDetail.DetailId).DetailName, selectDetail.Amount))
                        .ToList()
                };

                if (report != null)
                {
                    return report;
                }
                else
                {
                    throw new Exception("Error");
                }
            }
            return null;
        }

        public void SaveRequestToWord(LoadRequestReportViewModel request, string fileName)
        {
            var word = new Microsoft.Office.Interop.Word.Application();

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }            object missing = System.Reflection.Missing.Value;

            Document document = word.Documents.Add(ref missing, ref missing, ref missing, ref missing);

            var paragraph = document.Paragraphs.Add(missing);
            var range = paragraph.Range;

            range.Text = "Заявка от : " + request.DateCreate;
            var font = range.Font;
            font.Size = 12;
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
            var table = document.Tables.Add(rangeTable, request.Details.Count(), 2, ref missing, ref missing);

            font = table.Range.Font;
            font.Size = 10;
            font.Name = "Times New Roman";

            var paragraphTableFormat = table.Range.ParagraphFormat;
            paragraphTableFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
            paragraphTableFormat.SpaceAfter = 0;
            paragraphTableFormat.SpaceBefore = 0;

            for (int i = 0; i < request.Details.Count(); ++i)
            {
                table.Cell(i + 1, 1).Range.Text = request.Details.ElementAt(i).Item1;
                table.Cell(i + 1, 2).Range.Text = request.Details.ElementAt(i).Item2.ToString();
            }

            table.Borders.InsideLineStyle = WdLineStyle.wdLineStyleInset;
            table.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
            paragraph = document.Paragraphs.Add(missing);
            range = paragraph.Range;

            range.InsertParagraphAfter();

            object fileFormat = WdSaveFormat.wdFormatXMLDocument;

            document.SaveAs(fileName, ref fileFormat, ref missing,
            ref missing, ref missing, ref missing, ref missing,
            ref missing, ref missing, ref missing, ref missing,
            ref missing, ref missing, ref missing, ref missing,
            ref missing);

            document.Close(ref missing, ref missing, ref missing);

            word.Quit();
        }

        public void SaveRequestToExcel(LoadRequestReportViewModel request, string fileName)
        {
            var excel = new Microsoft.Office.Interop.Excel.Application();
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
            var excelworksheet = (Worksheet)excelsheets.get_Item(1);
            excelworksheet.Cells.Clear();

            Microsoft.Office.Interop.Excel.Range excelcells = excelworksheet.get_Range("B2", "C2");
            excelcells.Merge(Type.Missing);
            excelcells.Font.Bold = true;
            excelcells.Value2 = "Заявка от : ";
            excelcells.Font.Name = "Times New Roman";
            excelcells.Font.Size = 10;
            excelcells.RowHeight = 30;

            excelcells = excelworksheet.get_Range("B3", "C3");
            excelcells.Merge(Type.Missing);
            excelcells.Font.Bold = true;
            excelcells.Value2 = request.DateCreate;
            excelcells.Font.Name = "Times New Roman";
            excelcells.Font.Size = 10;
            excelcells.RowHeight = 30;            int row = 4;            foreach (var element in request.Details)
            {
                excelworksheet.Cells[row, 2] = element.Item1;

                excelworksheet.Cells[row, 3] = element.Item2.ToString();

                row++;
            }

            var range = excelworksheet.get_Range("B2", "C" + row);
            (range).Cells.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            excel.Workbooks[1].Save();
            excel.Quit();
        }
    }
}
