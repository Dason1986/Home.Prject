using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Home.WPF
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Home.Win.Applicatioin.ComponentModel.Media media = new Win.Applicatioin.ComponentModel.Media();
            media.GetMediaFiles();
        }
    }
}
