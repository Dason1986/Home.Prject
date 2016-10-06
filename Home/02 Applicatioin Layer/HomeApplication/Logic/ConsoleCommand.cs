
using HomeApplication.Logic.IO;
using NLog;
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
            if (args == null || args.Length == 0)
            {
                BuindMenu();

            }



        }
        static CommandMenu[] menus = new CommandMenu[] {
            new CommandMenu() {
                Name = "掃描物理文件",
                Key = "1",
                CommandClassType = typeof(ScanderPhysicalFile),
                OptionBuilderType = typeof(ScanderPhysicalFileOptionCommandBuilder)
            } , new CommandMenu() {
                Name = "圖片文件分析",
                Key = "2",
                CommandClassType = typeof(PhotoFileAnalysis),
                OptionBuilderType = typeof(PhotoFileAnalysisOptionCommandBuilder)
            } , new CommandMenu() {
                Name = "圖片相似指纹生成",
                Key = "3",
                CommandClassType = typeof(PhotoSimilarBuildFingerprint),
                OptionBuilderType = typeof(EmptyOptionCommandBuilder)
            } , new CommandMenu() {
                Name = "圖片相似比较分析",
                Key = "4",
                CommandClassType = typeof(PhotoSimilar),
                OptionBuilderType = typeof(PhotoSimilarOptionCommandBuilder)
            }};
        class CommandMenu
        {
            public string Name { get; set; }
            public string Key { get; set; }
            public Type CommandClassType { get; set; }
            public Type OptionBuilderType { get; set; }
        }
        private void BuindMenu()
        {
            Console.Clear();
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("菜單");
            builder.AppendLine("--------------------------------");
            foreach (var item in menus)
            {
                builder.AppendFormat("{0} ) {1}", item.Key, item.Name);
                builder.AppendLine();
            }
            builder.AppendLine("--------------------------------");
            builder.Append("輸入:");
            Console.Write(builder.ToString());
            LabInput:
            var inputkey = Console.ReadLine();
            var itemmenu = menus.FirstOrDefault(n => n.Key == inputkey);
            if (itemmenu == null)
            {
                Console.WriteLine("輸入無效，請重新輸入。");
                //   Console.Write("input:");
                goto LabInput;
            }
            else
            {

                try
                {
                    var optionBuilder = Library.HelperUtility.FastReflectionExtensions.CreateInstance<IOptionCommandBuilder>(itemmenu.OptionBuilderType);
                    var logicService = Library.HelperUtility.FastReflectionExtensions.CreateInstance<ILogicService>(itemmenu.CommandClassType);
                    optionBuilder.RumCommandLine();
                    logicService.Option = optionBuilder.GetOption();
                    logicService.Run();
                    Console.Write("執行完成，按任意鍵繼續。");
                }
                catch (Exception ex)
                {

                    var Logger = LogManager.GetCurrentClassLogger();
                    Logger.Error(ex);
                    Console.Write("按任意鍵繼續。");
                    Console.Read();
                }
                BuindMenu();
            }
        }
    }
}
