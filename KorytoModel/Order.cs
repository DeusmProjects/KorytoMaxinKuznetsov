using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KorytoModel
{
    public class Order
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        [Required]
        public decimal TotalSum { get; set; }

        public OrderStatus OrderStatus { get; set; }

        [Required]
        public DateTime DateCreate { get; set; }

        public DateTime? DateImplement { get; set; }

        public virtual Client Client { get; set; }

        [ForeignKey("OrderId")]
        public virtual List<OrderCar> OrderCars { get; set; }
    }
}
