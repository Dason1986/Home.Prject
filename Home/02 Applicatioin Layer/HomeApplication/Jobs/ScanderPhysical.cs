using Home.DomainModel.Aggregates.FileAgg;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Home.DomainModel.Repositories;
using FileEx = Home.DomainModel.Aggregates.FileAgg.FileInfo;
using Library;
using Home.DomainModel.ModuleProviders;
using Library.HelperUtility;

namespace HomeApplication.Jobs
{
    public class ScanderPhysical : ScheduleJobProvider
    {

        protected void OnDowrok()
        {
            //if (string.IsNullOrEmpty(Option.Path)) throw new Exception("路徑爲空");
            //_path = Path.GetFullPath(Option.Path);
            //if (_path[0] == '\'' || _path[0] == '"') _path = _path.Substring(1, Option.Path.Length - 2);
            var provider = Bootstrap.Currnet.GetService<IFileManagentModuleProvider>();
            var systemParameter = provider.CreateSystemParameter();
            var setting = systemParameter.GetListByGroup("ScanderPhysical");
            _path = setting.Cast<string>("Path", "");
            if (!Directory.Exists(_path)) return;
            var storageEngineRepository = provider.CreateStorageEngine();
            _engin = storageEngineRepository.GetByPathEngine(_path);
            if (_engin == null)
            {
                _engin = new StorageEngine(CreatedInfo.ScanderPhysical)
                {
                    Root = _path,
                    Name = Path.GetDirectoryName(_path)
                };
                storageEngineRepository.Add(_engin);
                storageEngineRepository.UnitOfWork.Commit();
            }
            Scan(_path);
            var _rootpath = _engin.Root;
            var FilesRepository = provider.CreateFileInfo();
            foreach (var item in _files)
            {
            
                if (string.IsNullOrEmpty(item)) continue;
                if (_filterfile.Any(ff => item.EndsWith(ff, StringComparison.OrdinalIgnoreCase))) continue;
                var filepath = item.Replace(_rootpath, string.Empty);
                if (filepath.StartsWith(@"\") || filepath.StartsWith(@"\")) filepath = filepath.Substring(1);
                if (FilesRepository.FileExists(filepath)) continue;


                var fileinfo = new FileEx(CreatedInfo.PhotoFileAnalysis)
                {
                    Extension = Path.GetExtension(item),
                    FullPath = filepath,
                    FileName = Path.GetFileName(item),

                    EngineID = _engin.ID,
                    SourceType = Home.DomainModel.SourceType.ServerScand
                };

                //if (File.Exists(item))
                //{
                //    fileinfo.FileSize = item.Length;
                //}

                FilesRepository.Add(fileinfo);
            }
            provider.UnitOfWork.Commit();

        }
        private readonly string[] _filterfile = { ".DS_Store", "desktop.ini", "thumbs.db" };
        private string _path;
        private StorageEngine _engin;
        IList<string> _files = new List<string>();
        private void Scan(string dic)
        {
            var tmpfiles = Directory.EnumerateFiles(dic);
            foreach (var item in tmpfiles)
            {
                _files.Add(item);
            }

            var dirs = Directory.EnumerateDirectories(dic);
            foreach (var item in dirs)
            {
                Scan(item);
            }
        }

        public override void Initialize()
        {

        }

        public override void Execute(IJobExecutionContext context)
        {
            OnDowrok();
        }
    }
}
