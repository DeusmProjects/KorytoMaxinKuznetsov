using System;
using System.Collections.Generic;
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
        public static IReportService ReportService { get; } = new ReportServiceDB(DbContext);
        public static IRequestService RequestService { get; } = new RequestServiceDB(DbContext);
        public static IStatisticService StatisticService { get; } = new StatisticServiceDB(DbContext);
        public static ClientViewModel AuthClient { get; set; }
        public static ClientOrders ModelReport { get; set; }
    }
}