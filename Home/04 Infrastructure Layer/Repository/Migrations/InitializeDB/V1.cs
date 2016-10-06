using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Migrations
{
    partial class V1
    {
        private void InitSql()
        {
            SqlResource("Repository.Migrations.InitializeDB.init.sql");
        }
    }
}
