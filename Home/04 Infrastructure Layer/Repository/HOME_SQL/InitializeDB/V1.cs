using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home.Repository.HOME_SQL
{
    partial class V1_1_1
    {
        private void InitSql()
        {
            SqlResource("Home.Repository.HOME_SQL.InitializeDB.init.sql");
            SqlResource("Home.Repository.HOME_SQL.InitializeDB.CreateView.sql");
            SqlResource("Home.Repository.HOME_SQL.InitializeDB.TimeLineByYYYY.sql");
            SqlResource("Home.Repository.HOME_SQL.InitializeDB.TimeLineByYYYYMM.sql");
            SqlResource("Home.Repository.HOME_SQL.InitializeDB.TimeLineByYYYYMMDD.sql");
        }
    }

    
}