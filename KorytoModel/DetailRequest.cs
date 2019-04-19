using System.ComponentModel.DataAnnotations;

namespace KorytoModel
{
    public class DetailRequest
    {
        public int Id { get; set; }

        public int DetailId { get; set; }

        public int RequestId { get; set; }

        [Required]
        public int Amount { get; set; }

        public virtual Detail Detail { get; set; }

        public virtual Request Request { get; set; }
    }
}
