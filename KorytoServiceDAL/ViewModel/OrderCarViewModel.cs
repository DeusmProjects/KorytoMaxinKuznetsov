using System.ComponentModel;

namespace KorytoServiceDAL.ViewModel
{
    public class OrderCarViewModel
    {
        public int Id { get; set; }

        public int CarId { get; set; }

        public int OrderId { get; set; }

        [DisplayName("Автомобиль")]
        public string CarName { get; set; }

        [DisplayName("Количество")]
        public int Amount { get; set; }
    }
}
