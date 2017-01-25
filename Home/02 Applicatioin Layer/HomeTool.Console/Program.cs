using HomeApplication;
using HomeApplication.Logic;
using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeTool.Console
{
    class Program
    {

        static int Main(string[] args)
        {
            if (args == null || args.Length == 0) return 0;
            ConsoleAppBootstrap boot = new ConsoleAppBootstrap();
            boot.Run();
            var arg1 = args[0];
            switch (arg1)
            {
                case "-x":
                    {
                        ConsoleCommand cmd = new ConsoleCommand(args);
                        cmd.Run();


                        break;
                    }
                case "-version": break;
                case "-help": break;
                default:
                    break;
            }

            return 0;

        }
    }
}
