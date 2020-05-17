using System.Collections.Generic;
using System.ComponentModel;

namespace KorytoServiceDAL.ViewModel
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        [DisplayName("ФИО Клиента")]
        public string ClientFIO { get; set; }

        public List<OrderCarViewModel> OrderCars { get; set; }

        [DisplayName("Сумма")]
        public decimal TotalSum { get; set; }

        [DisplayName("Статус заказа")]
        public string StatusOrder { get; set; }

        [DisplayName("Дата создания заказа")]
        public string DateCreate { get; set; }

        [DisplayName("Дата завершения заказа")]
        public string DateImplement { get; set; }
    }
}
