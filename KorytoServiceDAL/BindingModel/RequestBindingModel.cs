using System;
using System.Collections.Generic;

namespace KorytoServiceDAL.BindingModel
{
    public class RequestBindingModel
    {
        public int Id { get; set; }

        public DateTime DateCreate { get; set; }

        public List<DetailRequestBindingModel> DetailRequests { get; set; }
    }
}