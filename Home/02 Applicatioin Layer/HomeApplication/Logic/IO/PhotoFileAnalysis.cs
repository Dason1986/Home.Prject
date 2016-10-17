﻿using DomainModel.Aggregates.GalleryAgg;
using DomainModel.DomainServices;
using DomainModel.ModuleProviders;
using DomainModel.Repositories;
using Library;
using Library.ComponentModel.Logic;
using NLog.Fluent;
using Repository;
using Repository.ModuleProviders;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading;
//using static Library.Draw.ImageExif;

namespace HomeApplication.Logic.IO
{

    public class PhotoFileAnalysis : BaseMultiThreadingLogicService
    {
        public PhotoFileAnalysisOption Option
        {
            get { return _option; }
            set
            {
                _option = value;

            }
        }

        PhotoFileAnalysisOption _option;
        protected override IOption ServiceOption
        {
            get
            {
                return Option;
            }

            set
            {

                Option = (PhotoFileAnalysisOption)value;
            }
        }
        PhotoFileAnalysisProvider photoFileAnalysisProvider;

        protected override bool OnVerification()
        {
            switch (Option.SourceType)
            {
                case PhotoFileAnalysisSrouceType.Db:
                    photoFileAnalysisProvider = new PhotoFileAnalysisByDb(this);
                    break;
                case PhotoFileAnalysisSrouceType.File:
                    photoFileAnalysisProvider = new PhotoFileAnalysisByConfigFile(this);
                    break;
                default:
                    break;
            }

            return base.OnVerification();
        }

        protected override int GetTotalRecord()
        {
            return photoFileAnalysisProvider.GetTotalRecord();
        }
        protected override void ThreadProssSize(int beginindex, int endindex)
        {
            photoFileAnalysisProvider.ThreadProssSize(beginindex, endindex);
        }






        abstract class PhotoFileAnalysisProvider
        {
            protected PhotoFileAnalysis Analysis { get; private set; }
            public PhotoFileAnalysisProvider(PhotoFileAnalysis analysis)
            {
                Analysis = analysis;
            }
            public abstract int GetTotalRecord();
            public abstract void ThreadProssSize(int beginindex, int endindex);
        }

        class PhotoFileAnalysisByDb : PhotoFileAnalysisProvider
        {


            string[] filenames;

            public PhotoFileAnalysisByDb(PhotoFileAnalysis analysis) : base(analysis)
            {
            }


            public override int GetTotalRecord()
            {
                var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();
                IFileInfoRepository _filesRepository = provider.CreateFileInfo();

                var filecount = _filesRepository.GetFilesByExtensions(Analysis.Option.ImageTypes).Count();
                return filecount;
            }

            public override void ThreadProssSize(int beginindex, int endindex)
            {
                Analysis.Logger.Trace(string.Format("beginindex:{0} endindex:{1}", beginindex, endindex), 4);
                #region MyRegion


                var take = Analysis.BatchSize;



                var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();
                IFileInfoRepository _filesRepository = provider.CreateFileInfo();


                var photolist = _filesRepository.GetFilesByExtensions(Analysis.Option.ImageTypes).Skip(beginindex).Take(take).ToList();
                var domainService = Bootstrap.Currnet.GetService<IAddPhotoDomainService>();
                domainService.ModuleProvider = provider;
                foreach (var item in photolist)
                {             
                    domainService.Handle(item.Photo, item);
                }
                #endregion
                GC.Collect();
            }
        }
        class PhotoFileAnalysisByConfigFile : PhotoFileAnalysisProvider
        {


            string[] filenames;

            public PhotoFileAnalysisByConfigFile(PhotoFileAnalysis analysis) : base(analysis)
            {
            }


            public override void ThreadProssSize(int beginindex, int endindex)
            {
                Analysis.Logger.Trace(string.Format("beginindex:{0} endindex:{1}", beginindex, endindex), 4);
                #region MyRegion


                var take = Analysis.BatchSize;
                var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();
                IFileInfoRepository _filesRepository = provider.CreateFileInfo();
                var files = filenames.Skip(beginindex).Take(take).ToList();
                var domainService = Bootstrap.Currnet.GetService<IAddPhotoDomainService>();
                domainService.ModuleProvider = provider;
                //index = index + size;
                foreach (var file in files)
                {
                    DomainModel.Aggregates.FileAgg.FileInfo item = _filesRepository.GetByFullPath(file);
                    if (item == null)
                    {
                        Analysis.Logger.Warn("記錄不存在！" + file);
                        continue;
                    }
                    domainService.Handle(item.Photo, item);     

                }
                #endregion
                GC.Collect();
            }



            public override int GetTotalRecord()
            {
                var file = Analysis.Option.FileListPath;
                if (System.IO.File.Exists(file))
                {
                    filenames = System.IO.File.ReadAllLines(file);
                    return filenames.Length;
                }
                return 0;
            }
        }
    }



    public struct PhotoFileAnalysisOption : IOption
    {
        public string FileListPath { get; set; }
        public PhotoFileAnalysisSrouceType SourceType { get; set; }
        public string[] ImageTypes { get; set; }
    }
    public enum PhotoFileAnalysisSrouceType
    {
        Db,
        File
    }
    public class PhotoFileAnalysisOptionCommandBuilder : IOptionCommandBuilder<PhotoFileAnalysisOption>
    {
        public PhotoFileAnalysisOption GetOption()
        {
            return _option;
        }
        PhotoFileAnalysisOption _option;
        IOption IOptionCommandBuilder.GetOption()
        {
            return _option;
        }
        public void RumCommandLine()
        {
            _option = new PhotoFileAnalysisOption();
            Console.Write("是否使用默认条件（Y）：");
            var key = Console.ReadKey();
            Console.WriteLine();
            if (key.Key == ConsoleKey.Y)
            {
                //  Console.WriteLine();
                _option.ImageTypes = new string[] { ".jpg", ".png", ".gif", ".jpeg", ".bmp" };
                _option.SourceType = PhotoFileAnalysisSrouceType.Db;
                return;
            }

            {
                LabSource:

                Console.Write("圖像文件來源（0:db,1:txt文件）：");
                var sourcetype = Console.ReadLine();
                switch (sourcetype)
                {
                    case "0": _option.SourceType = PhotoFileAnalysisSrouceType.Db; break;
                    case "1": _option.SourceType = PhotoFileAnalysisSrouceType.File; break;
                    default:
                        goto LabSource;
                }

            }
            switch (_option.SourceType)
            {
                case PhotoFileAnalysisSrouceType.Db:
                    {
                        Console.Write("是否使用默认条件（Y）：");

                        if (Console.ReadKey().Key == ConsoleKey.Y)
                        {
                            //  Console.WriteLine();
                            _option.ImageTypes = new string[] { ".jpg", ".png", ".gif", ".jpeg", ".bmp" };

                            return;
                        }
                        LabCmd:
                        Console.Write("輸入圖像類型（,分隔）：");
                        var path = Console.ReadLine();
                        if (string.IsNullOrEmpty(path))
                        {
                            Console.WriteLine("不能爲空！");
                            goto LabCmd;
                        }

                        _option.ImageTypes = path.Split(',');
                    }
                    break;
                case PhotoFileAnalysisSrouceType.File:
                    {
                        LabCmd:
                        Console.Write("輸入文件列表路徑：");
                        var path = Console.ReadLine();
                        if (string.IsNullOrEmpty(path))
                        {
                            Console.WriteLine("不能爲空！");
                            goto LabCmd;
                        }
                        if (!System.IO.File.Exists(path))
                        {
                            Console.WriteLine("文件不存在！");
                            goto LabCmd;
                        }
                        _option.FileListPath = path;
                    }
                    break;
                default:
                    break;
            }


        }
    }

}