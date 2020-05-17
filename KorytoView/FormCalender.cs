using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KorytoView
{
    public partial class FormCalender : Form
    {
        public DateTime Model
        {
            set { model = value; }
            get
            {
                return model;
            }
        }
        private DateTime model;

        public FormCalender()
        {
            InitializeComponent();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            DateTime date = monthCalendar1.SelectionStart;
            textBox1.Text = monthCalendar1.SelectionStart.ToShortDateString();
            if (textBox1.Text == null)
            {
                throw new Exception("Дата не указана");
            }
            else
            {
                model = date;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (model != null)
                {
                    MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
              MessageBoxIcon.Error);
            }       
        }
    }
}
