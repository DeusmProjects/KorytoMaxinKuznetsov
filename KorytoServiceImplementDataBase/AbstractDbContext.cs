using KorytoModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KorytoServiceImplementDataBase
{
    public class AbstractDbContext : DbContext
    {
        public AbstractDbContext() : base("AbstractDatabase")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
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

