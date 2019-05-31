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
    public partial class FormMain : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly IMainService mainService;

        public FormMain()
        {
            InitializeComponent();
        }

        private void клиентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormClients>();
            form.ShowDialog();
        }

        private void машиныToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormCars>();
            form.ShowDialog();
        }

        private void деталиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormDetails>();
            form.ShowDialog();
        }

        private void заявкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormRequests>();
            form.ShowDialog();
        }

        private void buttonCreateOrder_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormCreateOrder>();
            form.ShowDialog();
        }

        //TODO : создать формочку и генератор отчетов
        private void покупкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Создать формочку и генератор отчетов", "TODO :", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //TODO : создать формочку и генератор отчетов
        private void заявкиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Создать формочку и генератор отчетов", "TODO :", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
