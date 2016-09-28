using HomeApplication.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeTool.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleCommand cmd = new ConsoleCommand(args);
            cmd.Run();

            System.Console.ReadLine();
        }
    }
}
