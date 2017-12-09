using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Home.DomainModel.Aggregates.FileAgg;
using Library.Domain.DomainEvents;
using Home.DomainModel.DomainServices;
using System.IO;
using NPOI.XWPF.Extractor;

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
            using (var storage = CurrnetFile.GetStorage())
            {
                if (!storage.Exists)
                    throw new PhotoDomainServiceException(Resources.DomainServiceResource.FileNotExist, new FileNotFoundException(CurrnetFile.FullPath));


                fs = storage.Get();
                string content = null;

                var iszip = SharpCompress.Archives.Zip.ZipArchive.IsZipFile(fs);
                fs.Seek(0, SeekOrigin.Begin);
                if (iszip)
                {
                    switch (CurrnetFile.Extension.ToLower())
                    {
                        case ".docx":
                        case ".dotx":
                        case ".doc":
                        case ".dot":
                            {
                                var document = new NPOI.XWPF.UserModel.XWPFDocument(fs);
                                var properties = document.GetProperties();
                                var UnderlyingProperties = properties.CoreProperties.GetUnderlyingProperties();
                                XWPFWordExtractor ex = new XWPFWordExtractor(document);
                                content = ex.Text;
                                document.Close();
                                break;
                            }
                        
                        case ".xlsx":
                        case ".xltx":
                        case ".xls":
                        case ".xlt":
                            {

                                var document = new NPOI.XSSF.UserModel.XSSFWorkbook(fs);
                                var properties = document.GetProperties();
                                var UnderlyingProperties = properties.CoreProperties.GetUnderlyingProperties();
                                var ex = new NPOI.XSSF.Extractor.XSSFExcelExtractor(document);
                                content = ex.Text;

                                document.Close();
                                break;
                            } 

                        default:
                            break;
                    }
                }
                else
                {
                    switch (CurrnetFile.Extension.ToLower())
                    {
                        case ".docx":
                        case ".dotx":
                        case ".doc":
                        case ".dot":
                            {

                                var ex = new NPOI.HWPF.Extractor.WordExtractor(fs);
                                var summaryInformation = ex.DocSummaryInformation;
                                content = ex.Text;

                                break;
                            }
                        case ".xlsx":
                        case ".xltx": 
                        case ".xls":
                        case ".xlt":
                            {

                                var document = new NPOI.HSSF.UserModel.HSSFWorkbook(fs);

                                var ex = new NPOI.HSSF.Extractor.ExcelExtractor(document);
                                content = ex.Text;
                                var documentSummary = ex.DocSummaryInformation;
                                document.Close();
                                break;
                            }

                        default:
                            break;
                    }
                }
               

                fs.Dispose();
            }
        }
    }

}
