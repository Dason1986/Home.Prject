using System;
using Library.Domain.DomainEvents;
using Home.DomainModel.DomainServices;
using System.IO;
using System.Collections.Generic;
using Home.DomainModel.Aggregates.OfficeAgg;

namespace HomeApplication.DomainServices
{
    public class OfficeFileDomainService : FileManagentDomainService, IOfficeFileDomainService
    {
        public Home.DomainModel.Aggregates.FileAgg.FileInfo CurrnetFile { get; private set; }

        protected override void Handle(DomainEventArgs args)
        {
            throw new NotImplementedException();
        }
        public void Handle(Home.DomainModel.Aggregates.FileAgg.FileInfo file)
        {
            CurrnetFile = file;

            DoAddAction();
        }

        private void DoAddAction()
        {
            if (CurrnetFile == null || CurrnetFile.Extension == null) return;
            Stream fs = null;
            ComponentModel.IO.OfficeAnalysis analysis = null;
            using (var storage = CurrnetFile.GetStorage())
            {
                if (!storage.Exists)
                    throw new PhotoDomainServiceException(Resources.DomainServiceResource.FileNotExist, new FileNotFoundException(CurrnetFile.FullPath));


                fs = storage.Get();
                var size = storage.Size();
                CurrnetFile.FileSize = size;
                CurrnetFile.MD5 = Library.HelperUtility.FileUtility.FileMD5(fs);
                if (this.FilesRepository.FileExists(CurrnetFile.MD5, size))
                {
                    storage.Delete();
                    CurrnetFile.FileStatue = Home.DomainModel.FileStatue.Duplicate;
                    CurrnetFile.StatusCode = Library.ComponentModel.Model.StatusCode.Delete;
                    this.FileModuleProvider.UnitOfWork.Commit();
                    return;
                }
                try
                {
                    analysis = ComponentModel.IO.OfficeAnalysisFactory.Get(CurrnetFile.Extension, fs);
                    analysis.Handle();
                }
                catch (Exception ex)
                {
                    CurrnetFile.FileStatue = Home.DomainModel.FileStatue.NotSupport;
                    CurrnetFile.StatusCode = Library.ComponentModel.Model.StatusCode.Disabled;

                    this.FileModuleProvider.UnitOfWork.Commit();
                    Logger.Error(ex, CurrnetFile.FileName);
                    throw ex;
                }
                finally
                {

                    fs.Dispose();
                }
                WordInfo word = new WordInfo(CreatedInfo.OfficeFileAnalysis)
                {
                    OffileFileType = analysis.OffileFileType,
                    FileID = CurrnetFile.ID
                };
                word.Summary = new Home.DomainModel.Aggregates.OfficeAgg.OfficeInfo
                {
                    Author = analysis.Author,
                    Subject = analysis.Subject,
                    Keyworks = analysis.Keywords,
                    Title = analysis.Title,
                    Content = analysis.Content,
                };

                if (analysis.Properties != null)
                {
                    ICollection<WordAttribute> Attributes = new List<WordAttribute>();
                    foreach (var item in analysis.Properties)
                    {
                        var value = item.Value != null ? item.Value.ToString() : string.Empty;
                        if (string.IsNullOrEmpty(value)|| value=="-1" || value == "0") continue;
                        Attributes.Add(new WordAttribute(CreatedInfo.OfficeFileAnalysis) { AttKey = item.Key, AttValue = value, OwnerID = word.ID });
                    }
                    word.Attributes = Attributes;
                }
                word.Summary.Substring();
                CurrnetFile.OfficeFile = word;



            }
        }

    }
}
