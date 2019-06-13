using KorytoModel;
using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace KorytoServiceImplementDataBase
{
    public class KorytoDbContext : DbContext
    {
        public KorytoDbContext() : base("KorytoDataBase")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDllIsCopied = SqlProviderServices.Instance;
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Detail> Details { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<CarDetail> CarDetails { get; set; }
        public virtual DbSet<OrderCar> OrderCars { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<DetailRequest> DetailRequests { get; set; }
    }
}

