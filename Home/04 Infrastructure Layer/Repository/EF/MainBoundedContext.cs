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
  
    public class MainBoundedContext : DbContext, IDbContext
    {
        static MainBoundedContext()
        {
       
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

      

        protected MainBoundedContext(string connection)
            : base(connection)
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public IQueryable<TEntity> CreateSet<TEntity>() where TEntity : class
        {
            return CreateSet<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //刪除未使用的約定
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Configurations.AddFromAssembly(this.GetType().Assembly);
            //EF.Mapping.TypeConfiguration.ModelCreating(modelBuilder);
        }

        
    }
}