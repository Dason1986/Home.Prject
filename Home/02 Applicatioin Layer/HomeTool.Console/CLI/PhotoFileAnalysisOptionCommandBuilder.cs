using Library.ComponentModel.Logic;
using System;
using System.IO;

namespace HomeApplication.Logic.IO
{
    public abstract class OptionCommandBuilder : IOptionCommandBuilder
    {
        public TextReader In { get; protected internal set; }
        public TextWriter Out { get; protected internal set; }

        public abstract void RumCommandLine();

        IOption IOptionCommandBuilder.GetOption()
        {
            return GetOptionImpl();
        }

        protected abstract IOption GetOptionImpl();
    }

    public class PhotoFileAnalysisOptionCommandBuilder : OptionCommandBuilder, IOptionCommandBuilder<PhotoFileAnalysisOption>
    {
        public PhotoFileAnalysisOption GetOption()
        {
            return _option;
        }

        private PhotoFileAnalysisOption _option;

        protected override IOption GetOptionImpl()
        {
            return _option;
        }

        public override void RumCommandLine()
        {
            _option = new PhotoFileAnalysisOption();
            Out.Write("是否使用默认条件（Y）：");
            var key = In.ReadLine();
            Out.WriteLine();
            if (key.ToUpper() == "Y")
            {
                //  Console.WriteLine();
                _option.ImageTypes = new string[] { ".jpg", ".png", ".gif", ".jpeg", ".bmp" };
                /*   _option.SourceType = PhotoFileAnalysisSrouceType.Db;*/
                return;
            }

            LabCmd:
            Out.Write("輸入圖像類型（,分隔）：");
            var path = Console.ReadLine();
            if (string.IsNullOrEmpty(path))
            {
                Console.WriteLine("不能爲空！");
                goto LabCmd;
            }

            _option.ImageTypes = path.Split(',');

            /*
            {
                LabSource:

                Out.Write("圖像文件來源（0:db,1:txt文件,2:目錄）：");
                var sourcetype = In.ReadLine();
                switch (sourcetype)
                {
                    case "0": _option.SourceType = PhotoFileAnalysisSrouceType.Db; break;
                    case "1": _option.SourceType = PhotoFileAnalysisSrouceType.File; break;
                    case "2": _option.SourceType = PhotoFileAnalysisSrouceType.Dir; break;
                    default:
                        goto LabSource;
                }
            }
            switch (_option.SourceType)
            {
                case PhotoFileAnalysisSrouceType.Db:
                    {
                        Out.Write("是否使用默认条件（Y）：");

                        if (In.ReadLine().ToUpper() == "Y")
                        {
                            //  Console.WriteLine();
                            _option.ImageTypes = new string[] { ".jpg", ".png", ".gif", ".jpeg", ".bmp" };

                            return;
                        }
                        LabCmd:
                        Out.Write("輸入圖像類型（,分隔）：");
                        var path = Console.ReadLine();
                        if (string.IsNullOrEmpty(path))
                        {
                            Console.WriteLine("不能爲空！");
                            goto LabCmd;
                        }

                        _option.ImageTypes = path.Split(',');
                        break;
                    }

                case PhotoFileAnalysisSrouceType.File:
                    {
                        LabCmd:
                        Out.Write("輸入文件列表路徑：");
                        var path = In.ReadLine();
                        if (string.IsNullOrEmpty(path))
                        {
                            Out.WriteLine("不能爲空！");
                            goto LabCmd;
                        }
                        if (!System.IO.File.Exists(path))
                        {
                            Out.WriteLine("文件不存在！");
                            goto LabCmd;
                        }
                        _option.FileListPath = path;
                        break;
                    }

                case PhotoFileAnalysisSrouceType.Dir:
                    {
                        LabCmd:
                        Out.Write("輸入指定掃描目標：");
                        var path = In.ReadLine();
                        if (string.IsNullOrEmpty(path))
                        {
                            Out.WriteLine("不能爲空");
                            goto LabCmd;
                        }
                        if (!Directory.Exists(path))
                        {
                            Out.WriteLine("目錄不存在");
                            goto LabCmd;
                        }
                        _option.DirPath = path;

                        Out.Write("是否使用默认文件類型（Y）：");

                        if (In.ReadLine().ToUpper() == "Y")
                        {
                            //  Console.WriteLine();
                            _option.ImageTypes = new string[] { ".jpg", ".png", ".gif", ".jpeg", ".bmp" };

                            return;
                        }
                        LabImageTypes:
                        Out.Write("輸入圖像類型（,分隔）：");
                        var imageTypes = In.ReadLine();
                        if (string.IsNullOrEmpty(imageTypes))
                        {
                            Out.WriteLine("不能爲空！");
                            goto LabImageTypes;
                        }

                        _option.ImageTypes = imageTypes.Split(',');
                        break;
                    }
                default:
                    break;
            }*/
        }
    }
}