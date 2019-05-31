using KorytoServiceDAL.BindingModel;
using KorytoServiceDAL.Interfaces;
using KorytoServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace KorytoView
{
    public partial class FormCar : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private int? id;
        private List<CarDetailViewModel> carDetails;
        private readonly ICarService car;

        public FormCar(ICarService car)
        {
            InitializeComponent();
            this.car = car;
        }

        private void FormCar_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    CarViewModel carView = car.GetElement(id.Value);
                    if (carView != null)
                    {
                        textBoxName.Text = carView.CarName;
                        textBoxYear.Text = carView.Year.ToString();
                        textBoxPrice.Text = carView.Price.ToString();
                        carDetails = carView.CarDetails;
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
                carDetails = new List<CarDetailViewModel>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (carDetails != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = carDetails;
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
            var form = Container.Resolve<FormCarDetail>();

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                    {
                        form.Model.CarId = id.Value;
                    }

                    carDetails.Add(form.Model);

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
                        carDetails.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
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
                var form = Container.Resolve<FormCarDetail>();

                form.Model = carDetails[dataGridView.SelectedRows[0].Cells[0].RowIndex];

                if (form.ShowDialog() == DialogResult.OK)
                {
                    carDetails[dataGridView.SelectedRows[0].Cells[0].RowIndex] = form.Model;
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
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxYear.Text))
            {
                MessageBox.Show("Заполните год выпускак", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (carDetails == null || carDetails.Count == 0)
            {
                MessageBox.Show("Заполните детали", "Ошибка", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<CarDetailBindingModel> carDetailsBinding = new List<CarDetailBindingModel>();

                for (int i = 0; i < carDetails.Count; ++i)
                {
                    carDetailsBinding.Add(new CarDetailBindingModel
                    {
                        Id = carDetails[i].Id,
                        CarId = carDetails[i].CarId,
                        DetailId = carDetails[i].DetailId,
                        Amount = carDetails[i].Amount, 
                    });
                }

                if (id.HasValue)
                {
                    car.UpdateElement(new CarBindingModel
                    {
                        Id = id.Value,
                        CarName = textBoxName.Text,
                        Year = Convert.ToInt32(textBoxYear.Text),
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        CarDetails = carDetailsBinding
                    });
                }
                else
                {
                    car.AddElement(new CarBindingModel
                    {
                        CarName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        Year = Convert.ToInt32(textBoxYear.Text),
                        CarDetails = carDetailsBinding
                    });
                }

                Console.WriteLine();

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
