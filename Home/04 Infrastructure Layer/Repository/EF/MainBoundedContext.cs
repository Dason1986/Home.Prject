
using Library.Domain.Data;
using Repository.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class MainBoundedContext : DbContext, IDbContext
    {
        static MainBoundedContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MainBoundedContext, Configuration>());
        }

        public MainBoundedContext()
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        protected MainBoundedContext(DbConnection existingConnection)
            : base(existingConnection, true)
        {
        }

        protected MainBoundedContext(string connection)
            : base(connection)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //刪除未使用的約定
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            EF.Mapping.TypeConfiguration.ModelCreating(modelBuilder);
        }

        IUnitOfWork IDbContext.CreateUnitOfWork()
        {
            return new EFUnitOfWork(this);
        }
    }
}
