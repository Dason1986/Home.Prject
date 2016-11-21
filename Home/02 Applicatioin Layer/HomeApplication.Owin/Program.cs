using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HomeApplication.Owin
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Microsoft.Owin.Hosting.WebApp.Start<Startup1>("http://localhost:9000"))
            {
             
                Console.WriteLine("Press [enter] to quit...");
                Console.ReadLine();
			
            }
          
        }
    }
}
