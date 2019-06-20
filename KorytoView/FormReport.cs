using KorytoServiceDAL.BindingModel;
using KorytoServiceDAL.Interfaces;
using KorytoServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KorytoServiceImplementDataBase.Implementations;
using Unity;

namespace KorytoView
{
    public partial class FormReport : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly IReportService report;

        private ReportBindingModel reportModel;
        private List<LoadRequestReportViewModel> DetailsOrder;
        private List<LoadOrderReportViewModel> DetailsRequest;

        public FormReport(IReportService report)
        {
            InitializeComponent();
            this.report = report;
            reportModel = new ReportBindingModel();
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonTo_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormCalender>();

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    reportModel.DateTo = form.Model;
                    textBoxTo.Text = form.Model.ToShortDateString();
                }
            }
        }

        private void buttonFrom_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormCalender>();

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    reportModel.DateFrom = form.Model;
                    textBoxFrom.Text = form.Model.ToShortDateString();
                }
            }
        }

        private void buttonCreateReport_Click(object sender, EventArgs e)
        {
            DetailsOrder = report.GetDetailsRequest(reportModel);
            DetailsRequest = report.GetDetailsOrder(reportModel);
            if (DetailsOrder != null && DetailsRequest != null)
            {
                MessageBox.Show("Отчёт успешно создан", "Информация", MessageBoxButtons.OK);
                LoadData(DetailsOrder, DetailsRequest);
            }
            else
            {
                throw new Exception("Error");
            }

            LoadData(DetailsOrder, DetailsRequest);
        }

        private void LoadData(List<LoadRequestReportViewModel> DetailsRequest, List<LoadOrderReportViewModel> DetailsOrder)
        {
            try
            {
                if (DetailsOrder != null)
                {

                    dataGridView.RowCount = 100;
                    dataGridView.ColumnCount = 3;

                    dataGridView.Columns[0].Visible = true;
                    dataGridView.Columns[1].Visible = true;
                    dataGridView.Columns[2].Visible = true;
                    dataGridView.Columns[0].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
                    dataGridView.Columns[1].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
                    dataGridView.Columns[2].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;

                    int nextAction = 0;

                    dataGridView.Rows[nextAction].Cells[1].Value = "Ревизия";
                    nextAction += 2;

                    dataGridView.Rows[nextAction].Cells[1].Value = "Пополнения";
                    nextAction += 2;

                    for (int element = 0; element < DetailsRequest.Count; element++)
                    {
                        int rowAction = nextAction;
                        dataGridView.Rows[rowAction].Cells[0].Value = "Заявка от : ";
                        rowAction++;
                        dataGridView.Rows[rowAction].Cells[0].Value = DetailsRequest[element].DateCreate;
                        rowAction++;
                        dataGridView.Rows[rowAction].Cells[0].Value = "Деталь";
                        dataGridView.Rows[rowAction].Cells[1].Value = "Количество";
                        dataGridView.Rows[rowAction].Cells[2].Value = "Итог";
                        rowAction++;
                        var details = DetailsRequest[element].Details;
                        foreach (var detail in details)
                        {
                            dataGridView.Rows[rowAction].Cells[0].Value = detail.Item1;
                            dataGridView.Rows[rowAction].Cells[1].Value = detail.Item2;
                            dataGridView.Rows[rowAction].Cells[2].Value = detail.Item2;
                            rowAction++;
                        }
                        nextAction = rowAction + 1;
                    }

                    nextAction += 2;
                    dataGridView.Rows[nextAction].Cells[1].Value = "Расходы";
                    nextAction += 2;

                    for (int element = 0; element < DetailsOrder.Count; element++)
                    {
                        int rowAction = nextAction;
                        dataGridView.Rows[rowAction].Cells[0].Value = "Заказ №";
                        dataGridView.Rows[rowAction].Cells[1].Value = DetailsOrder[element].OrderId;
                        rowAction++;
                        dataGridView.Rows[rowAction].Cells[0].Value = "Дата : ";
                        dataGridView.Rows[rowAction].Cells[1].Value = DetailsOrder[element].DateCreate;
                        rowAction++;
                        dataGridView.Rows[rowAction].Cells[0].Value = "Авто : ";
                        dataGridView.Rows[rowAction].Cells[1].Value = DetailsOrder[element].CarName;
                        rowAction++;
                        dataGridView.Rows[rowAction].Cells[0].Value = "Количество : ";
                        dataGridView.Rows[rowAction].Cells[1].Value = DetailsOrder[element].CarAmount;
                        rowAction++;
                        dataGridView.Rows[rowAction].Cells[0].Value = "Деталь";
                        dataGridView.Rows[rowAction].Cells[1].Value = "Количество";
                        dataGridView.Rows[rowAction].Cells[2].Value = "Итог";
                        rowAction++;
                        var details = DetailsOrder[element].Details;
                        foreach (var detail in details)
                        {
                            dataGridView.Rows[rowAction].Cells[0].Value = detail.DetailName;
                            dataGridView.Rows[rowAction].Cells[1].Value = detail.Amount;
                            dataGridView.Rows[rowAction].Cells[2].Value = detail.Amount * DetailsOrder[element].CarAmount;
                            rowAction++;
                        }
                        nextAction = rowAction + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFile = new SaveFileDialog()
            {
                Filter = "PDF file|*.pdf",
                ValidateNames = true
            })
            {
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        report.SaveDetailsReport(DetailsOrder, DetailsRequest, saveFile.FileName, reportModel);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK);
                    }

                }

                string directoryPath = saveFile.FileName;

                var files = new List<string>
                        {
                            directoryPath
                        };

                MailService.SendEmail("kuzikuzikuzikuzi33@gmail.com", "Оповещение по заявкам",
                    $"Отчет от {reportModel.DateFrom} по {reportModel.DateTo}", files);

                MessageBox.Show("Отчёт успешно сохранен и отправлен получателю", "Информация", MessageBoxButtons.OK);
            }
        }
    }
}
