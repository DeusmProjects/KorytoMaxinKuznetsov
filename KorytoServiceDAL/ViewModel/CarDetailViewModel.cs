using System.ComponentModel;

namespace KorytoServiceDAL.ViewModel
{
    public class CarDetailViewModel
    {
        public int Id { get; set; }

        public int CarId { get; set; }

        public int DetailId { get; set; }

        [DisplayName("Деталь")]
        public string DetailName { get; set; }

        [DisplayName("Количество")]
        public int Amount { get; set; }
    }
}
