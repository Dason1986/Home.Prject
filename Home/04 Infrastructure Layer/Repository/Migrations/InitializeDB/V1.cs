using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Migrations
{
    partial class V1
    {
        private void InitSql()
        {
            //        var migration = (System.Data.Entity.Migrations.Infrastructure.IDbMigration)this;
            //        migration.AddOperation(new CreateViewOperation("EquipmentView", @"select a1.photoid, a1.attvalue 'Make',a2.attvalue as 'model'  from (select * from photoattribute where attkey='EquipmentMake') as  a1 ,
            //(select * from photoattribute where attkey = 'EquipmentModel') as a2
            //where a1.photoid = a2.photoid"));
            SqlResource("Home.Repository.Migrations.InitializeDB.CreateView.sql");
            SqlResource("Home.Repository.Migrations.InitializeDB.init.sql");
        }
    }
    public class CreateViewOperation : MigrationOperation
    {
        public CreateViewOperation(string viewName, string viewQueryString)
          : base(null)
        {
            ViewName = viewName;
            ViewString = viewQueryString;
        }
        public string ViewName { get; private set; }
        public string ViewString { get; private set; }
        public override bool IsDestructiveChange
        {
            get { return false; }
        }
    }
}
