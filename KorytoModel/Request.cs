using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KorytoModel
{
    public class Request
    {
        public int Id { get; set; }

        public DateTime DateCreate { get; set; }

        [ForeignKey("RequestId")]
        public virtual List<DetailRequest> DetailRequests { get; set; }
    }
}
