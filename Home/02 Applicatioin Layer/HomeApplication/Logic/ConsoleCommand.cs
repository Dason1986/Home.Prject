
using HomeApplication.Logic.IO;
using Library.ComponentModel.Logic;
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
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            this.args = args;
           
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception)
            {
                var Logger = LogManager.GetCurrentClassLogger();
                Logger.Error(e.ExceptionObject as Exception, "未知錯誤！");
            }
        }

        public void Run()
        {
            if (args == null || args.Length == 0)
            {
                BuindMenu(_menus);

            }



        }
        static CommandMenu[] _menus = new CommandMenu[] {
            new CommandMenu() {
                Name = "掃描物理文件",
                Key = ConsoleKey.D1,
                CommandClassType = typeof(ScanderPhysicalFile),
                OptionBuilderType = typeof(ScanderPhysicalFileOptionCommandBuilder)
            } ,

           new ChildCommandMenu {
               Key =ConsoleKey.D2,
               Name ="圖片處理",
               Menus = new CommandMenu[]
               {
                   new CommandMenu() {
                                        Name = "圖片文件分析",
                                        Key = ConsoleKey.D1,
                                        CommandClassType = typeof(PhotoFileAnalysis),
                                        OptionBuilderType = typeof(PhotoFileAnalysisOptionCommandBuilder)
                                    } ,
                   new CommandMenu() {
                                        Name = "圖片相似指纹生成",
                                        Key = ConsoleKey.D2,
                                        CommandClassType = typeof(PhotoSimilarBuildFingerprint),
                                        OptionBuilderType = typeof(EmptyOptionCommandBuilder)
                                    } ,
                   new CommandMenu() {
                                        Name = "圖片相似比较分析",
                                        Key = ConsoleKey.D3,
                                        CommandClassType = typeof(PhotoSimilar),
                                        OptionBuilderType = typeof(PhotoSimilarOptionCommandBuilder)
                                    }
               }
           } ,
            new CommandMenu() {
                Name = "刪除重複文件信息(MD5)",
                Key = ConsoleKey.D3,
                CommandClassType = typeof(DeleteFileDistinctByMD5),
                OptionBuilderType = typeof(EmptyOptionCommandBuilder)
            }};
        class CommandMenu
        {
            public string Name { get; set; }
            public ConsoleKey Key { get; set; }
            public Type CommandClassType { get; set; }
            public Type OptionBuilderType { get; set; }
        }
        class ChildCommandMenu : CommandMenu
        {
            public CommandMenu[] Menus { get; set; }
        }
        private void BuindMenu(CommandMenu[] menus)
        {
            Console.Clear();
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("菜單");
            builder.AppendLine("--------------------------------");
            foreach (var item in menus)
            {
                builder.AppendFormat("{0} ) {1}", GetShow(item.Key), item.Name);
                builder.AppendLine();
            }
            builder.AppendLine("--------------------------------");
            builder.Append("輸入:");
            Console.Write(builder.ToString());
            LabInput:
            var inputkey = Console.ReadKey().Key;
            Console.WriteLine();
            if (inputkey == ConsoleKey.Escape)
            {
                BuindMenu(_menus);
            }
            var itemmenu = menus.FirstOrDefault(n => n.Key == inputkey);
            if (itemmenu == null)
            {
                Console.WriteLine("輸入無效，請重新輸入。");
                //   Console.Write("input:");
                goto LabInput;
            }
            else
            {
                if (itemmenu is ChildCommandMenu)
                {
                    BuindMenu(((ChildCommandMenu)itemmenu).Menus);
                }
                else
                {
                    try
                    {
                        var optionBuilder = Library.HelperUtility.FastReflectionExtensions.CreateInstance<IOptionCommandBuilder>(itemmenu.OptionBuilderType);
                        var logicService = Library.HelperUtility.FastReflectionExtensions.CreateInstance<ILogicService>(itemmenu.CommandClassType);
                        optionBuilder.RumCommandLine();
                        logicService.Option = optionBuilder.GetOption();
                        logicService.Start();
                        Console.Write("執行完成，按任意鍵繼續。");
                    }
                    catch (Exception ex)
                    {

                        var Logger = LogManager.GetCurrentClassLogger();
                        Logger.Error(ex);
                        Console.Write("按任意鍵繼續。");
                        Console.Read();
                    }
                    BuindMenu(menus);
                }


            }
        }

        private object GetShow(ConsoleKey key)
        {
            switch (key)
            {

                case ConsoleKey.D0:

                case ConsoleKey.D1:

                case ConsoleKey.D2:

                case ConsoleKey.D3:

                case ConsoleKey.D4:

                case ConsoleKey.D5:

                case ConsoleKey.D6:

                case ConsoleKey.D7:

                case ConsoleKey.D8:

                case ConsoleKey.D9:
                    return key.ToString()[1];

                default:
                    return key;

            }
        }
    }
}
