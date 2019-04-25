using System.Collections.Generic;

namespace KorytoServiceDAL.BindingModel
{
    public class OrderBindingModel
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public decimal TotalSum { get; set; }

        public virtual List<OrderCarBindingModel> OrderCars { get; set; }
    }
}
