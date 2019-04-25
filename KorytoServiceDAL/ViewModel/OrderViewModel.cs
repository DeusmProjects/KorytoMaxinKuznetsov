using System.Collections.Generic;
using System.ComponentModel;

namespace KorytoServiceDAL.ViewModel
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        [DisplayName("ФИО Клиента")]
        public string CustomerFIO { get; set; }

        public List<OrderCarViewModel> OrderCars { get; set; }

        [DisplayName("Сумма")]
        public decimal TotalSum { get; set; }

        [DisplayName("Статус заказа")]
        public string StatusOrder { get; set; }

        [DisplayName("Дата создания заказа")]
        public string DateCreateOrder { get; set; }

        [DisplayName("Дата завершения заказа")]
        public string DateImplementOrder { get; set; }
    }
}
