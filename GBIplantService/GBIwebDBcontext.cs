using GBIplantModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBIplantService
{
    [Table("GBIwebDatabase")]
    public class GBIwebDBcontext: DbContext
    {
        public GBIwebDBcontext()
        {
            //настройки конфигурации для entity
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public virtual DbSet<Buyer> Buyers { get; set; }

        public virtual DbSet<GBIindgridient> GBIindgridients { get; set; }

        public virtual DbSet<Executor> Executors { get; set; }

        public virtual DbSet<Zakaz> Zakazes { get; set; }

        public virtual DbSet<GBIpieceOfArt> GBIpieceOfArts { get; set; }

        public virtual DbSet<GBIpieceofArt__ingridient> GBIpieceofArt__ingridients { get; set; }

        public virtual DbSet<Storage> Storages { get; set; }

        public virtual DbSet<Storage__GBIingridient> Storage__GBIingridients { get; set; }
    }
}
