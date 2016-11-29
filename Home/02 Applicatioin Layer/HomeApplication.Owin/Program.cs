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
            var settingPort = System.Configuration.ConfigurationManager.AppSettings["Port"];
            var port = Library.HelperUtility.StringUtility.TryCast(settingPort, 9000).Value;
            var option = new StartOptions("http://localhost:" + port)
            {
                ServerFactory = "Microsoft.Owin.Host.HttpListener"
            };
            option.Urls.Add(string.Format("http://{0}:{1}", "127.0.0.1", port));
            var hostname = System.Net.Dns.GetHostName();

            option.Urls.Add(string.Format("http://{0}:{1}", hostname, port));
            foreach (var address in System.Net.Dns.GetHostAddresses(hostname))
            {
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    option.Urls.Add(string.Format("http://{0}:{1}", address, port));
            }

            using (WebApp.Start<Startup1>(option))
            {

                foreach (var item in option.Urls)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("Press [enter] to quit...");
                Console.ReadLine();


            }

        }
    }
}
