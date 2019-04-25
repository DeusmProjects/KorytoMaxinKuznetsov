using System.ComponentModel;

namespace KorytoServiceDAL.ViewModel
{
    public class DetailRequestViewModel
    {
        public int Id { get; set; }

        public int DetailId { get; set; }

        public int RequestId { get; set; }

        [DisplayName("Количество деталей")]
        public int Amount { get; set; }
    }
}
