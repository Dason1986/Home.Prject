using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using System.ServiceProcess;

namespace HomeApplication.Owin
{
    class Program
    {
        static void Main(string[] args)
        {
            if (System.Environment.UserInteractive)
            {
                StartOptions option = OwinAppBootstrap.CraeteStratOptions();

                using (WebApp.Start(option))
                {

                    foreach (var item in option.Urls)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("Press [enter] to quit...");
                    Console.ReadLine();


                }
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                      new HomeOwinService()
                };
                ServiceBase.Run(ServicesToRun);


            }
        }


    }
}
