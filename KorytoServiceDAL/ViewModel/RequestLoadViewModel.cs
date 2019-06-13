using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KorytoServiceDAL.ViewModel
{
    public class RequestLoadViewModel
    {
        public string DateRequst { get; set; }
        public IEnumerable<Tuple<string, int>> Details { get; set; }
    }
}
