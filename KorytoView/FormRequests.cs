using KorytoServiceDAL.Interfaces;
using KorytoServiceDAL.ViewModel;
using KorytoServiceImplementDataBase.Implementations;
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
    public partial class FormRequests : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly IRequestService request;

        public FormRequests(IRequestService request)
        {
            InitializeComponent();
            this.request = request;
        }

        private void LoadData()
        {
            try
            {
                List<RequestViewModel> list = request.GetList();
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
              MessageBoxIcon.Error);
            }
        }

        private void buttonAddElement_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormRequest>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void buttonDeleteElement_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                    try
                    {
                        request.DeleteElement(id);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void FormRequests_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonMailExcel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Создать отчет в Excel", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);

                    using (SaveFileDialog saveFile = new SaveFileDialog()
                    {
                        Filter = "XLS file|*.xls",
                        ValidateNames = true
                    })
                    {
                        var data = request.GetDetailsRequest(id);

                        if (saveFile.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {
                                request.SaveRequestToExcel(request.GetDetailsRequest(id), saveFile.FileName);                              
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
                            $"Заявка №{id} от {data.DateCreate}", files);

                        MessageBox.Show("Отчёт успешно сохранен и отправлен получателю", "Информация", MessageBoxButtons.OK);
                    }
                }
            }
        }

        private void buttonMailWord_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Создать отчет в Excel", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);

                    using (SaveFileDialog saveFile = new SaveFileDialog()
                    {
                        Filter = "DOC file|*.doc",
                        ValidateNames = true
                    })
                    {

                        var data = request.GetDetailsRequest(id);

                        if (saveFile.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {
                                request.SaveRequestToWord(request.GetDetailsRequest(id), saveFile.FileName);
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
                            $"Заявка №{id} от {data.DateCreate}", files);

                        MessageBox.Show("Заявка успешно сохранена и отправлена поставшику", "Информация", MessageBoxButtons.OK);
                    }
                }
            }
        }
    }
}
