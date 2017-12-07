using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Migrations
{
    public partial class V1_2_1
    {

        void ExSqlUp()
        {
          
            SqlResource("Home.Repository.Migrations.V1_2_1.up.sql");
            SqlResource("Home.Repository.Migrations.V1_2_1.CreateView.sql");


        }
        void ExSqlDown()
        {
            SqlResource("Home.Repository.Migrations.V1_2_1.down.sql");
        }
    }
}
