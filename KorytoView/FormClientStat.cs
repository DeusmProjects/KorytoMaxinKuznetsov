using KorytoServiceDAL.Interfaces;
using System;
using System.Globalization;
using System.Windows.Forms;
using Unity;

namespace KorytoView
{
    public partial class FormClientStat : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set => id = value; }
        private int? id;
        private readonly IStatisticService statistic;

        public FormClientStat(IStatisticService statistic)
        {
            InitializeComponent();
            this.statistic = statistic;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormClientStat_Load(object sender, EventArgs e)
        {
            if (!id.HasValue) return;
            try
            {
                var average = statistic.AverageCustomerCheck(id.Value);
                textBoxAverage.Text = average.ToString(CultureInfo.InvariantCulture);

                var countAllCars = statistic.GetClientCarsCount(id.Value);
                textBoxAllCars.Text = countAllCars.ToString();

                var popCar = statistic.PopularCar(id.Value);
                textBoxPopular.Text = popCar;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
