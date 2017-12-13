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
 
            var ass = this.GetType().Assembly;
            var sqlfiles = ass.GetManifestResourceNames();
            foreach (var item in sqlfiles.Where(n => n.StartsWith("Home.Repository.HOME_SQL.InitializeDB.")))
            {

                SqlResource(item, ass);
            }

        }
    }

    
}