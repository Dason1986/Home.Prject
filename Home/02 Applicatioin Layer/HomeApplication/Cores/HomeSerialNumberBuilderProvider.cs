using Home.DomainModel.Aggregates.SystemAgg;
using Home.DomainModel.Repositories;
using HomeApplication.Interceptors;
using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApplication.Cores
{
    public class HomeSerialNumberBuilderProvider : SerialNumberBuilderProvider
    {
        public HomeSerialNumberBuilderProvider()
        {
            ID = Guid.NewGuid();
        }

        public Guid ID { get; private set; }
        private readonly NLog.ILogger _logger = NLog.LogManager.GetCurrentClassLogger();

        public override void Initialize()
        {
            _logger.Trace("Get SerialNumber config");
            ISerialNumberManagementRepository serialNumberManagementRepository = Bootstrap.Currnet.GetService<ISerialNumberManagementRepository>();

            var list = serialNumberManagementRepository.GetAll().ToArray();

            _logger.Trace("Create SerialNumber Builder");
            foreach (var serialNumberManagement in list)
            {
                if (serialNumberManagement.IsCustom)
                {
                    var customType = Type.GetType(serialNumberManagement.CustomClass);
                    var objCustomClass = Library.Bootstrap.Currnet.GetService(customType);
                    if (objCustomClass is SerialNumberBuilder)
                    {
                        var numberBuilder = objCustomClass as MEGASerialNumberBuilder;

                        _builderCollections.Add(numberBuilder);
                    }
                    else
                    {
                        throw new LogicException("不存在相關的自定生成模組");
                    }
                }
                else
                {
                    var newitem = new MEGASerialNumberBuilder(serialNumberManagement.Code)
                    {
                        Id = serialNumberManagement.ID
                    };
                    newitem.SetOption(serialNumberManagement.SerialNumberFormat, serialNumberManagement.CurrentNumber, 10);

                    _builderCollections.Add(newitem);
                }
            }
        }
        public override int Count { get { return _builderCollections.Count; } }
        public override string[] GetBuilderCodes()
        {
            return _builderCollections.Select(n => n.Name).ToArray();
        }
        public override string Dequeue(string name)
        {
            lock (Lockobj)
            {
                var builder = GetBuilder(name) as MEGASerialNumberBuilder;
                if (builder == null) throw new ArgumentNullException("name");

                var number = builder.Dequeue();
                ISerialNumberManagementRepository serialNumberManagementRepository = Bootstrap.Currnet.GetService<ISerialNumberManagementRepository>();
                var serialNumberManagement = serialNumberManagementRepository.Get(builder.Id);
                if (serialNumberManagement == null)
                {
                    throw new Exception("序號生成配置信息不存在");
                }
                serialNumberManagement.CurrentNumber = builder.CurrentNumber;
                //      serialNumberManagementRepository.(serialNumberManagement);
                serialNumberManagementRepository.UnitOfWork.Commit();
                serialNumberManagementRepository.UnitOfWork.Dispose();
                return number;
            }
        }

        private readonly ICollection<MEGASerialNumberBuilder> _builderCollections = new List<MEGASerialNumberBuilder>();
        private static readonly object Lockobj = new object();

        public override SerialNumberBuilder GetBuilder(string code)
        {
            var builder = _builderCollections.FirstOrDefault(n => n.Name == code);
            return builder;
        }

        public override string Dequeue(string name, string serialNumberFormat)
        {
            if (string.IsNullOrEmpty(serialNumberFormat)) return Dequeue(name);
            lock (Lockobj)
            {
                var builder = GetBuilder(name) as MEGASerialNumberBuilder;
                ISerialNumberManagementRepository serialNumberManagementRepository = Bootstrap.Currnet.GetService<ISerialNumberManagementRepository>();
                SerialNumberManagement serialNumberManagement = null;
                if (builder == null)
                {
                    serialNumberManagement = new SerialNumberManagement(CreatedInfo.SerialNumber);
                    //serialNumberManagement = new SerialNumberManagement();
                    serialNumberManagement.SerialNumberFormat = serialNumberFormat;
                    serialNumberManagement.Code = name;
                    serialNumberManagementRepository.Add(serialNumberManagement);
                    builder = new MEGASerialNumberBuilder(name);
                    builder.Id = serialNumberManagement.ID;
                    builder.SetOption(serialNumberManagement.SerialNumberFormat, serialNumberManagement.CurrentNumber, 10);
                    _builderCollections.Add(builder);
                    serialNumberManagementRepository.UnitOfWork.Commit();
                }
                else
                {
                    serialNumberManagement = serialNumberManagementRepository.Get(builder.Id);
                }

                if (serialNumberManagement == null)
                {
                    throw new Exception("序號生成配置信息不存在");
                }
                var number = builder.Dequeue();
                serialNumberManagement.CurrentNumber = builder.CurrentNumber;
                // serialNumberManagementRepository.Modify(serialNumberManagement);
                serialNumberManagementRepository.UnitOfWork.Commit();

                return number;
            }
        }

        private class MEGASerialNumberBuilder : Library.SerialNumberBuilder
        {
            public MEGASerialNumberBuilder(string name)
            {
                Name = name;
            }

            public Guid Id { get;  set; }
            public string Name { get; private set; }
        }
    }
}
