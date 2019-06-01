using KorytoServiceDAL.Interfaces;
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
    public partial class FormClientStat : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private int? id;
        private readonly IStatistic statistic;

        public FormClientStat(IStatistic statistic)
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
            if (id.HasValue)
            {
                try
                {
                    decimal average = statistic.AverageCustomerCheck(id.Value);
                    textBoxAverage.Text = average.ToString();

                    int countAllCars = statistic.HowManyCarTheClient(id.Value);
                    textBoxAllCars.Text = countAllCars.ToString();

                    string popCar = statistic.PopularCar(id.Value);
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
}
