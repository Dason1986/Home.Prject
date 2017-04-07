using Library.Domain.Data;
using Library.Domain.Data.EF;
using Repository.Migrations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    // [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class MainBoundedContext : EFContext, IDbContext
    {
        static MainBoundedContext()
        {
        //    var dbbtype = System.Configuration.ConfigurationManager.AppSettings.Get("DBType") ?? "MSSQL";
            var setting = System.Configuration.ConfigurationManager.ConnectionStrings["MainBoundedContext"] as ConnectionStringSettings;
            switch (setting.ProviderName)
            {
                case "MySql.Data.MySqlClient":
                    Database.SetInitializer(new MigrateDatabaseToLatestVersion<MainBoundedContext, Repository.Migrations.Configuration>()); break;
                case "System.Data.SqlClient":
                default:
                    Database.SetInitializer(new MigrateDatabaseToLatestVersion<MainBoundedContext, Home.Repository.HOME_SQL.Configuration>());
                    break;
            }
        }

        public MainBoundedContext()
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        protected MainBoundedContext(DbConnection existingConnection)
            : base(existingConnection)
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
            modelBuilder.Configurations.AddFromAssembly(this.GetType().Assembly);
            //EF.Mapping.TypeConfiguration.ModelCreating(modelBuilder);
        }

        IUnitOfWork IDbContext.CreateUnitOfWork()
        {
            return new EFUnitOfWork(this);
        }
    }
}