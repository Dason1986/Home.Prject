using Home.DomainModel.Aggregates.FileAgg;
using Home.DomainModel.ModuleProviders;
using Library.ComponentModel.Model;
using Library.Domain.DomainEvents;

namespace Home.DomainModel.DomainServices
{
    public interface IAddFileDomainService : IDomainService
    {
        void Handle(AddFileEventArgs args);

          IFileManagentModuleProvider FileModuleProvider { get; set; }
    }

    public class AddFileEventArgs : DomainEventArgs
    {
        public AddFileEventArgs(StorageEngine engine, MemoryFile[] memoryFiles, SourceType sourceType, ICreatedInfo createdInfo)
        {
            Engine = engine;
            MemoryFiles = memoryFiles;
            SourceType = sourceType;
            CreatedInfo = createdInfo;
        }

        public AddFileEventArgs(StorageEngine engine, System.IO.FileInfo[] physicalFiles, SourceType sourceType, ICreatedInfo createdInfo)
        {
            Engine = engine;
            PhysicalFiles = physicalFiles;
            SourceType = sourceType;
            CreatedInfo = createdInfo;
        }

        public StorageEngine Engine { get; protected set; }
        public ICreatedInfo CreatedInfo { get; protected set; }
        public SourceType SourceType { get; protected set; }
        public MemoryFile[] MemoryFiles { get; protected set; }
        public System.IO.FileInfo[] PhysicalFiles { get; protected set; }
    }

    public struct MemoryFile
    {
        public string Name { get; set; }
        public byte[] Buffer { get; set; }
    }
}