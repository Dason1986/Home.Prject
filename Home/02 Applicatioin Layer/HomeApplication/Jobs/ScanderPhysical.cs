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
using HomeApplication.DomainServices;

namespace HomeApplication.Jobs
{
    [PersistJobDataAfterExecution]
    [DisallowConcurrentExecution]
    public class ScanderPhysical : ScheduleJobProvider
    {
        readonly static object _sync = new object();
        protected void OnDowrok()
        {
            lock (_sync)
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

                AddFileDomainService addFileDomain = new AddFileDomainService();
                addFileDomain.FileModuleProvider = provider;
                for (int i = 0; i < _files.Count; i = i + 100)
                {
                    var files = _files.Skip(i).Take(100).Select(n => new System.IO.FileInfo(n)).ToArray();
                    var args = new Home.DomainModel.DomainServices.AddFileEventArgs(
                    _engin,
                     files,
                  Home.DomainModel.SourceType.PC,
                  CreatedInfo.ScanderPhysical);
                    addFileDomain.Handle(args);
                }


            }
        }

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
