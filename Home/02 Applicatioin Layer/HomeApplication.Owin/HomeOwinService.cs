using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace HomeApplication.Owin
{
    partial class HomeOwinService : ServiceBase
    {
        public HomeOwinService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: 在此处添加代码以启动服务。
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

        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
        }
    }
}
