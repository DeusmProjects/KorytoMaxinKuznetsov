using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KorytoModel
{
    public class OrderCar
    {
        public int Id { get; set; }

        public int CarId { get; set; }

        public int OrderId { get; set; }

        [Required]
        public int Amount { get; set; }

        public virtual Car Car { get; set; }

        public virtual Order Order { get; set; }
    }
}
