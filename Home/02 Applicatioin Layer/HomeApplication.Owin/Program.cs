using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace HomeApplication.Owin
{
    class Program
    {
        static void Main(string[] args)
        {
			var option=new StartOptions("http://localhost:9000");
			option.Urls.Add(string.Format("http://{0}:9000",System.Net.Dns.GetHostName()));
			foreach (var address in System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName()))
			{
				if(address.AddressFamily== System.Net.Sockets.AddressFamily.InterNetwork)
				option.Urls.Add(string.Format("http://{0}:9000", address));
			}

			using (WebApp.Start<Startup1>(option))
            {

				foreach (var item in option.Urls)
				{
					Console.WriteLine(item);
				}
                Console.WriteLine("Press [enter] to quit...");
                Console.ReadLine();

				while (true) { System.Threading.Thread.Sleep(500);}
            }
          
        }
    }
}
