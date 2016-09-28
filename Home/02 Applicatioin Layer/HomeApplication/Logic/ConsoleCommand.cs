using HomeApplication.IO;
using Repository;
using Repository.ModuleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApplication.Logic
{
    public class ConsoleCommand
    {
        private string[] args;

        public ConsoleCommand(string[] args)
        {
            this.args = args;
        }

        public void Run()
        {
            ScanderPhysicalFile scander = new ScanderPhysicalFile() { Option=new ScanderPhysicalFileOption {Path= args[0] } };
            scander.Run();
          
         
        }
    }
}
