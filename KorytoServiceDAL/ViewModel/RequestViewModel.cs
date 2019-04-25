using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace KorytoServiceDAL.ViewModel
{
    public class RequestViewModel
    {
        public int Id { get; set; }

        [DisplayName("Дата оформления")]
        public DateTime DateCreate { get; set; }

        public virtual List<DetailRequestViewModel> DetailRequests { get; set; }
    }
}
