
using System.ComponentModel;

namespace KorytoServiceDAL.ViewModel
{
    public class CarViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название автомобиля")]
        public string CarName { get; set; }

        [DisplayName("Цена автомобиля")]
        public decimal Price { get; set; }

        [DisplayName("Год выпуска")]
        public int Year { get; set; }
    }
}
