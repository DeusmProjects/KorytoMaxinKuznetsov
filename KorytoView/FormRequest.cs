using KorytoModel;
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
    public partial class FormRequest : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly IRequestService request;
        public int Id { set { id = value; } }
        private int? id;
        private List<DetailRequestViewModel> details;


        public FormRequest(IRequestService request)
        {
            InitializeComponent();
            this.request = request;
        }

        private void FormRequest_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    RequestViewModel requestView = request.GetElement(id.Value);
                    if (requestView != null)
                    {
                        textBoxDate.Text = requestView.DateCreate.ToShortDateString();
                        details = requestView.DetailRequests;
                        LoadData();
                    }

                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
            else
            {
                textBoxDate.Text = DateTime.Now.ToShortDateString();
                details = new List<DetailRequestViewModel>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (details != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = details;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
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
            var form = Container.Resolve<FormDetailRequest>();

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                    {
                        form.Model.RequestId = id.Value;
                    }

                    details.Add(form.Model);

                }
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
                    try
                    {
                        details.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
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

        private void buttonChangeElement_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormDetailRequest>();

                form.Model = details[dataGridView.SelectedRows[0].Cells[0].RowIndex];

                if (form.ShowDialog() == DialogResult.OK)
                {
                    details[dataGridView.SelectedRows[0].Cells[0].RowIndex] = form.Model;
                    LoadData();
                }
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxDate.Text))
            {
                MessageBox.Show("Не указана дата", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (details == null || details.Count == 0)
            {
                MessageBox.Show("Заполните детали", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<DetailRequestBindingModel> requestDetailsBinding = new List<DetailRequestBindingModel>();

                for (int i = 0; i < details.Count; ++i)
                {
                    requestDetailsBinding.Add(new DetailRequestBindingModel
                    {
                        Id = details[i].Id,
                        DetailId = details[i].DetailId,
                        Amount = details[i].Amount,
                    });
                }

                request.AddElement(new RequestBindingModel
                {
                    DateCreate = DateTime.Now,
                    DetailRequests = requestDetailsBinding
                });

                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }


    }
}
