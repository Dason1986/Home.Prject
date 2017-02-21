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
            SqlResource("Home.Repository.Migrations.InitializeDB.init.sql");
            SqlResource("Home.Repository.Migrations.InitializeDB.CreateView.sql");
            SqlResource("Home.Repository.Migrations.InitializeDB.TimeLineByYYYY.sql");
            SqlResource("Home.Repository.Migrations.InitializeDB.TimeLineByYYYYMM.sql");
            SqlResource("Home.Repository.Migrations.InitializeDB.TimeLineByYYYYMMDD.sql");
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