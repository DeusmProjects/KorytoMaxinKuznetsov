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
    public partial class FormClient : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private int? id;
        private readonly IClientService client;

        public FormClient(IClientService client)
        {
            InitializeComponent();
            this.client = client;
        }

        private void button_CANCEL_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    client.UpdateElement(new ClientBindingModel
                    {
                        Id = id.Value,
                        ClientFIO = textBoxName.Text,
                        Mail = textBoxMail.Text,
                        Login = textBoxLogin.Text,
                        Password = textBoxPassword.Text
                    });
                }
                else

                {
                    client.AddElement(new ClientBindingModel
                    {
                        ClientFIO = textBoxName.Text,
                        Mail = textBoxMail.Text,
                        Login = textBoxLogin.Text,
                        Password = textBoxPassword.Text
                    });
                }
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

        private void FormClient_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    ClientViewModel clientViewModel = client.GetElement(id.Value);
                    if (clientViewModel != null)
                    {
                        textBoxName.Text = clientViewModel.ClientFIO;
                        textBoxMail.Text = clientViewModel.Mail;
                        textBoxLogin.Text = clientViewModel.Login;
                        textBoxPassword.Text = clientViewModel.Password;
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
