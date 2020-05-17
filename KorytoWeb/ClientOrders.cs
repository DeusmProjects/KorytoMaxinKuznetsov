using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KorytoServiceDAL.ViewModel;

namespace KorytoWeb
{
    public class ClientOrders
    {
        public List<OrderViewModel> Orders { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}