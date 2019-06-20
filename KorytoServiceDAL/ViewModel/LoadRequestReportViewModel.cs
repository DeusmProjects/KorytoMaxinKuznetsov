using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KorytoServiceDAL.ViewModel
{
    public class LoadRequestReportViewModel
    {
        /// <summary>
        /// Дата заявки
        /// </summary>
        public string DateCreate { get; set; }

        /// <summary>
        /// ( деталь, количество )
        /// </summary>
        public IEnumerable<Tuple<string, int>> Details { get; set; }
    }
}
