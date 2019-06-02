using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace KorytoModel
{
    [DataContract]
    public class Detail
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Required]
        public string DetailName { get; set; }

        [DataMember]
        [Required]
        public int TotalAmount { get; set; }

        [DataMember]
        [Required]
        public int TotalReserve { get; set; }

        [ForeignKey("DetailId")]
        public virtual List<CarDetail> CarDetails { get; set; }

        [ForeignKey("DetailId")]
        public virtual List<DetailRequest> DetailRequests { get; set; }
    }
}
