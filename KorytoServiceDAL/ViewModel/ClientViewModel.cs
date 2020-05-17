using System.ComponentModel;

namespace KorytoServiceDAL.ViewModel
{
    public class ClientViewModel
    {
        public int Id { get; set; }

        [DisplayName("ФИО Клиента")]
        public string ClientFIO { get; set; }

        [DisplayName("Логин клиента")]
        public string Login { get; set; }

        [DisplayName("Пароль клиента")]
        public string Password { get; set; }

        [DisplayName("Почта клиента")]
        public string Mail { get; set; }
    }
}
