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
        public const string Name = "HomeOwinService";
        protected override void OnStart(string[] args)
        {
            // TODO: 在此处添加代码以启动服务。
             OwinAppBootstrap.MVCHostStart(args);
 
        }

        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
        }
    }
}
