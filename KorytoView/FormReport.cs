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
using Unity;

namespace KorytoView
{
    public partial class FormReport : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly IReportService report;
        private ReportBindingModel reportModel;
        private List<RequestLoadViewModel> list;

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
            list = report.GetDetailReguest(reportModel);
            LoadData(list);
        }

        private void LoadData(List<RequestLoadViewModel> list)
        {
            try
            {
                if (list != null)
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

                    int saveIndexRow = 0;

                    for (int element = 0; element < list.Count; element++)
                    {
                        RequestLoadViewModel reportElement = list[element];

                        for (int row = saveIndexRow; row < 100; row++)
                        {

                            dataGridView.Rows[row].Cells[0].Value = "Отчет от : ";
                            row++;
                            dataGridView.Rows[row].Cells[0].Value = reportElement.DateRequst;

                            for (int record = 0; record < reportElement.Details.Count(); record++)
                            {

                                dataGridView.Rows[row].Cells[1].Value = reportElement.Details.ElementAt(record).Item1;
                                dataGridView.Rows[row].Cells[2].Value = reportElement.Details.ElementAt(record).Item2;
                                row++;
                            }
                            saveIndexRow = row;
                            break;
                        }
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
                Filter = "PDF file|*.pdf", ValidateNames = true
            })
            {
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        report.SaveLoadRequest(list, saveFile.FileName);
                        MessageBox.Show("Отчёт успешно создан", "Информация", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK);
                    } 
                   
                }
            }
        }
    }
}
