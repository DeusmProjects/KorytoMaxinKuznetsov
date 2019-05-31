using KorytoServiceDAL.Interfaces;
using KorytoServiceDAL.ViewModel;
using KorytoServiceImplementDataBase;
using KorytoServiceImplementDataBase.Implementations;

namespace KorytoWeb
{
    public class Globals
    {
        public static KorytoDbContext DbContext { get; } = new KorytoDbContext();
        public static IClientService ClientService { get; } = new ClientServiceDB(DbContext);
        public static ICarService CarService { get; } = new CarServiceDB(DbContext);
        public static IDetailService DetailService { get; } = new DetailServiceDB(DbContext);
        public static IMainService MainService { get; } = new MainServiceDB(DbContext);
        public static IRequestService RequestService { get; } = new RequestServiceDB(DbContext);
        public static IStatistic StatisticService { get; } = new StatisticServiceDB(DbContext);

        public static ClientViewModel AuthClient { get; set; } = null;
    }
}