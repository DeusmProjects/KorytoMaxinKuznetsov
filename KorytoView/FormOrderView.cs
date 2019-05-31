using KorytoModel;
using KorytoServiceDAL.Interfaces;
using KorytoServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace KorytoView
{
    public partial class FormOrderView : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private int? id;
        private List<OrderCarViewModel> pointsOfOrder;
        private readonly IMainService mainService;
        
        public FormOrderView(IMainService mainService)
        {
            InitializeComponent();
            this.mainService = mainService;
        }

        private void FormOrderView_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {                
                    Order order = mainService.GetElement(id.Value);

                    List<OrderCar> carOrder = order.OrderCars;

                    pointsOfOrder = new List<OrderCarViewModel>();

                    foreach (var element in carOrder)
                    {
                        pointsOfOrder.Add(new OrderCarViewModel
                        {
                            CarName = element.Car.CarName,
                            Amount = element.Amount
                        });
                    }

                    if (pointsOfOrder != null)
                    {
                        dataGridView.DataSource = pointsOfOrder;
                        dataGridView.Columns[0].Visible = false;
                        dataGridView.Columns[1].Visible = false;
                        dataGridView.Columns[2].Visible = false;
                        dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                    

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                  MessageBoxIcon.Error);
                }
            }
        }
    }
}
