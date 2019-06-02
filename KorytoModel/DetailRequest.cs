using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace KorytoModel
{
    [DataContract]
    public class DetailRequest
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int DetailId { get; set; }

        [DataMember]
        public int RequestId { get; set; }

        [DataMember]
        [Required]
        public int Amount { get; set; }

        public virtual Detail Detail { get; set; }

        public virtual Request Request { get; set; }
    }
}
