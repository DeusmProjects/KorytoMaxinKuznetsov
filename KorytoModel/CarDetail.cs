using System.ComponentModel.DataAnnotations;

namespace KorytoModel
{
    public class CarDetail
    {
        public int Id { get; set; }

        public int CarId { get; set; }

        public int DetailId { get; set; }

        [Required]
        public int Amount { get; set; }

        public virtual Car Car { get; set; }

        public virtual Detail Detail { get; set; }
    }
}
