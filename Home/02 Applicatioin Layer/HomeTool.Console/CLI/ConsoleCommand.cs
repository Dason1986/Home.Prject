using HomeApplication.Logic.IO;
using Library.ComponentModel.Logic;
using NLog;
using Home.Repository;
using Home.Repository.ModuleProviders;
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
            IsRunning = true;
            BuindMenu(_menus);
        }

        private static CommandMenu[] _menus = new CommandMenu[] {
            new CommandMenu() {
                Name = "掃描物理文件",
                Key = "1",
                CommandClassType = typeof(ScanderPhysicalFile),
                OptionBuilderType = typeof(ScanderPhysicalFileOptionCommandBuilder)
            } ,

           new ChildCommandMenu {
               Key ="2",
               Name ="圖片處理",
               Menus = new CommandMenu[]
               {
                   new CommandMenu() {
                                        Name = "圖片文件分析",
                                        Key = "1",
                                        CommandClassType = typeof(PhotoFileAnalysis),
                                        OptionBuilderType = typeof(PhotoFileAnalysisOptionCommandBuilder)
                                    } ,
                   new CommandMenu() {
                                        Name = "圖片相似指纹生成",
                                        Key = "2",
                                        CommandClassType = typeof(PhotoSimilarBuildFingerprint),
                                        OptionBuilderType = typeof(EmptyOptionCommandBuilder)
                                    } ,
                   new CommandMenu() {
                                        Name = "圖片相似比较分析",
                                        Key = "3",
                                        CommandClassType = typeof(PhotoSimilar),
                                        OptionBuilderType = typeof(PhotoSimilarOptionCommandBuilder)
                                    }
               }
           } ,
            new CommandMenu() {
                Name = "刪除重複文件信息(MD5)",
                Key = "3",
                CommandClassType = typeof(DeleteFileDistinctByMD5),
                OptionBuilderType = typeof(EmptyOptionCommandBuilder)
            }};

        public bool IsRunning { get; private set; }

        private class CommandMenu
        {
            public string Name { get; set; }
            public string Key { get; set; }
            public Type CommandClassType { get; set; }
            public Type OptionBuilderType { get; set; }
        }

        private class ChildCommandMenu : CommandMenu
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
                builder.AppendFormat("{0} ) {1}", item.Key, item.Name);
                builder.AppendLine();
            }
            builder.AppendLine("--------------------------------");
            builder.Append("輸入:");
            Console.Write(builder.ToString());
            LabInput:
            var inputkey = Console.ReadLine();
            Console.WriteLine();
            if (inputkey == "exit")
            {
                IsRunning = false;
                return;
                //      BuindMenu(_menus);
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
                        IOptionCommandBuilder optionBuilder = itemmenu.OptionBuilderType == typeof(EmptyOptionCommandBuilder) ? new EmptyOptionCommandBuilder() as IOptionCommandBuilder : Library.HelperUtility.FastReflectionExtensions.CreateInstance<OptionCommandBuilder>(itemmenu.OptionBuilderType) as IOptionCommandBuilder;

                        var logicService = Library.HelperUtility.FastReflectionExtensions.CreateInstance<ILogicService>(itemmenu.CommandClassType);
                        if (optionBuilder is OptionCommandBuilder)
                        {
                            var cmd = optionBuilder as OptionCommandBuilder;
                            cmd.In = Console.In;
                            cmd.Out = Console.Out;
                            optionBuilder.RumCommandLine();
                        }
                        logicService.Option = ((IOptionCommandBuilder)optionBuilder).GetOption();
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
    }
}