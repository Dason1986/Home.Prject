using Home.DomainModel.Aggregates.FileAgg;
using Home.DomainModel.DomainServices;
using Home.DomainModel.ModuleProviders;
using Home.DomainModel.Repositories;
using Library;
using Library.ComponentModel.Logic;
using Library.Domain.Data;
using Library.Infrastructure.Application;
using System;
using System.Linq;
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
                    photoFileAnalysisProvider = new PhotoFileAnalysisByFileList(this);
                    break;
                case PhotoFileAnalysisSrouceType.Dir:
                    photoFileAnalysisProvider = new PhotoFileAnalysisByDir(this);
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
        class PhotoFileAnalysisByDir : PhotoFileAnalysisByFilePath
        {
            public PhotoFileAnalysisByDir(PhotoFileAnalysis analysis) : base(analysis)
            {
            }

            public override int GetTotalRecord()
            {
                var path = Analysis.Option.DirPath;
                if (System.IO.Directory.Exists(path))
                {
                    Filenames = Analysis.Option.ImageTypes.SelectMany(n => System.IO.Directory.GetFiles(path, "*" + n, System.IO.SearchOption.AllDirectories)).ToArray();
                    return Filenames.Length;
                }
                return 0;
            }


        }
        class PhotoFileAnalysisByDb : PhotoFileAnalysisProvider
        {




            public PhotoFileAnalysisByDb(PhotoFileAnalysis analysis) : base(analysis)
            {
            }


            public override int GetTotalRecord()
            {
                var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();
                IFileInfoRepository _filesRepository = provider.CreateFileInfo();

                var filecount = _filesRepository.GetPhotoFilesByExtensions(Analysis.Option.ImageTypes).Count();
                return filecount;
            }

            public override void ThreadProssSize(int beginindex, int endindex)
            {
          
                #region MyRegion


                var take = Analysis.BatchSize;



                using (var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>())
                {
                    // IFileInfoRepository _filesRepository = provider.CreateFileInfo();


                    var domainService = Bootstrap.Currnet.GetService<IAddPhotoDomainService>();
                    domainService.ModuleProvider = provider;

                    //      domainService.SetDbConexnt(provider);
                    IFileInfoRepository _filesRepository = domainService.ModuleProvider.CreateFileInfo();
                    var photolist = _filesRepository.GetPhotoFilesByExtensions(Analysis.Option.ImageTypes).Skip(beginindex).Take(take).ToList();
                    foreach (var item in photolist)
                    {
                        domainService.Handle(item.Photo, item);
                        domainService.ModuleProvider.UnitOfWork.Commit();
                    }
                    GC.Collect();
                }
                #endregion
            }
        }
        abstract class PhotoFileAnalysisByFilePath : PhotoFileAnalysisProvider
        {
            public PhotoFileAnalysisByFilePath(PhotoFileAnalysis analysis) : base(analysis)
            {
            }

            protected string[] Filenames { get; set; }
            public override void ThreadProssSize(int beginindex, int endindex)
            {
               
                #region MyRegion


                var take = Analysis.BatchSize;
                using (var provider = Bootstrap.Currnet.GetService<IDbContext>())
                {

                    var files = Filenames.Skip(beginindex).Take(take).ToList();

                    IFileInfoRepository _filesRepository = Bootstrap.Currnet.GetService<IFileInfoRepository>(new string[] { "context" }, new object[] { provider });
                    //index = index + size;
                    foreach (var file in files)
                    {
                        if (!System.IO.File.Exists(file))
                        {
                            Analysis.Logger.WarnByContent("File not exitst!", file);
                            continue;
                        }
                        FileInfo item = _filesRepository.GetByFullPath(file);

                        if (item == null)
                        {
                            Analysis.Logger.WarnByContent("fileinfo not exist,create fileinfo", file  );
                            var fileinfo = new FileInfo(CreatedInfo.ScanderPhysical);
                            System.IO.FileInfo sysInfo = new System.IO.FileInfo(file);
                            fileinfo.Extension = sysInfo.Extension;
                            fileinfo.FullPath = file;
                            fileinfo.FileName = sysInfo.Name;
                            fileinfo.MD5 = Library.HelperUtility.FileUtility.FileMD5(file);
                            fileinfo.FileSize = sysInfo.Length;
                            if (_filesRepository.FileExists(fileinfo.MD5, fileinfo.FileSize)) fileinfo.StatusCode = Library.ComponentModel.Model.StatusCode.Disabled;
                            _filesRepository.Add(fileinfo);
                            item = fileinfo;
                            _filesRepository.UnitOfWork.Commit();
                        }
                        if (item.StatusCode == Library.ComponentModel.Model.StatusCode.Disabled) continue;
                        FileAggregateRoot fileagg = new FileAggregateRoot(item, _filesRepository);
                        fileagg.PublishPhotoDomain();

                    }
                    provider.Dispose();
                    GC.Collect();
                }
                #endregion

            }
        }
        class PhotoFileAnalysisByFileList : PhotoFileAnalysisByFilePath
        {




            public PhotoFileAnalysisByFileList(PhotoFileAnalysis analysis) : base(analysis)
            {
            }






            public override int GetTotalRecord()
            {
                var file = Analysis.Option.FileListPath;
                if (System.IO.File.Exists(file))
                {
                    Filenames = System.IO.File.ReadAllLines(file);
                    return Filenames.Length;
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
        public string DirPath { get;  set; }
    }
    public enum PhotoFileAnalysisSrouceType
    {
        Db,
        File,
        Dir
    }

}
