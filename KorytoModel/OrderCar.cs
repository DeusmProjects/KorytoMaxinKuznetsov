using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace KorytoModel
{
    [DataContract]
    public class OrderCar
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int CarId { get; set; }

        [DataMember]
        public int OrderId { get; set; }

        [DataMember]
        [Required]
        public int Amount { get; set; }

        public virtual Car Car { get; set; }

        public virtual Order Order { get; set; }
    }
}
