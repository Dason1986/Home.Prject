using System;
using System.IO;
using NPOI.XWPF.Extractor;
using Home.DomainModel;
using Home.DomainModel.Aggregates.FileAgg;
using Library.Domain.DomainEvents;
using Home.DomainModel.DomainServices;
using System.Collections.Generic;
using System.Text;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;

namespace HomeApplication.ComponentModel.IO
{
    public class OfficeAnalysisFactory
    {
        public static OfficeAnalysis Get(string extension, Stream stream)
        {
            OfficeAnalysis analysis = null;
            switch (extension.ToLower())
            {
                case ".pdf":
                    analysis = new PdfAnalysis(extension, stream);
                    break;
                case ".xlsx":
                case ".xltx":
                case ".xls":
                case ".xlt":
                    analysis = new XlsAnalysis(extension, stream);
                    break;
                case ".docx":
                case ".dotx":
                case ".doc":
                case ".dot":
                    analysis = new DocAnalysis(extension, stream);
                    break;
                case ".ppt":
                case ".pptx":
                    analysis = new PptAnalysis(extension, stream);
                    break;
                default:
                    throw new NotSupportedException(extension);
            }
            return analysis;
        }

    }
    public abstract class OfficeAnalysis
    {
        public OfficeAnalysis(string extension, Stream stream)
        {
            Extension = extension;
            this.IOStream = stream;
            Properties = new SortedDictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        }
        public string Extension { get; private set; }
        public Stream IOStream { get; private set; }
        public string Content { get; protected set; }
        public string Author { get; protected set; }
        public string Keywords { get; protected set; }
        public string Subject { get; protected set; }
        public string Title { get; protected set; }
        public IDictionary<string, object> Properties { get; set; }
        public OffileFileType OffileFileType { get; protected set; }

        public abstract void Handle();
    }
    class XlsAnalysis : OfficeAnalysis
    {


        public XlsAnalysis(string extension, Stream stream) : base(extension, stream)
        {
            OffileFileType = OffileFileType.XLS;
        }

        public override void Handle()
        {
            var iszip = SharpCompress.Archives.Zip.ZipArchive.IsZipFile(IOStream);
            IOStream.Seek(0, SeekOrigin.Begin);
            if (iszip)
            {


                var document = new NPOI.XSSF.UserModel.XSSFWorkbook(IOStream);
                var properties = document.GetProperties();
                var coreproperties = properties.CoreProperties;
                Properties.Add("Created", coreproperties.Created);
                Properties.Add("Category", coreproperties.Category);
                Properties.Add("ContentType", coreproperties.ContentType);
                Author = coreproperties.Creator;
                Properties.Add("Description", coreproperties.Description);
                Properties.Add("Identifier", coreproperties.Identifier);
                Keywords = coreproperties.Keywords;
                Properties.Add("LastPrinted", coreproperties.LastPrinted);
                Properties.Add("Modified", coreproperties.Modified);
                Properties.Add("Revision", coreproperties.Revision);
                Title = coreproperties.Title;
                Subject = coreproperties.Subject;
                var UnderlyingProperties = properties.CoreProperties.GetUnderlyingProperties();
                Properties.Add("Size", UnderlyingProperties.Size);

                var extendedProperties = properties.ExtendedProperties;
                Properties.Add("ApplicationName", extendedProperties.Application);
                Properties.Add("AppVersion", extendedProperties.AppVersion);
                Properties.Add("Characters", extendedProperties.Characters);
                Properties.Add("CharactersWithSpaces", extendedProperties.CharactersWithSpaces);
                Properties.Add("Company", extendedProperties.Company);
                Properties.Add("HiddenSlides", extendedProperties.HiddenSlides);
                Properties.Add("HyperlinkBase", extendedProperties.HyperlinkBase);
                Properties.Add("Lines", extendedProperties.Lines);
                Properties.Add("Manager", extendedProperties.Manager);
                Properties.Add("MMClips", extendedProperties.MMClips);
                Properties.Add("Notes", extendedProperties.Notes);
                Properties.Add("Pages", extendedProperties.Pages);
                Properties.Add("Paragraphs", extendedProperties.Paragraphs);
                Properties.Add("PresentationFormat", extendedProperties.PresentationFormat);
                Properties.Add("Slides", extendedProperties.Slides);
                Properties.Add("Template", extendedProperties.Template);
                Properties.Add("TotalTime", extendedProperties.TotalTime);
                Properties.Add("Words", extendedProperties.Words);

                var ex = new NPOI.XSSF.Extractor.XSSFExcelExtractor(document);
                Content = ex.Text;

                document.Close();


            }
            else
            {

                var document = new NPOI.HSSF.UserModel.HSSFWorkbook(IOStream);

                var ex = new NPOI.HSSF.Extractor.ExcelExtractor(document);
                Content = ex.Text;
                var summaryInformation = ex.SummaryInformation;
                Properties.Add("ApplicationName", summaryInformation.ApplicationName);
                Author = summaryInformation.Author;
                Properties.Add("ByteOrder", summaryInformation.ByteOrder);
                Properties.Add("CharCount", summaryInformation.CharCount);
                Properties.Add("Comments", summaryInformation.Comments);
                Properties.Add("CreateDateTime", summaryInformation.CreateDateTime);
                Properties.Add("EditTime", summaryInformation.EditTime);
                Properties.Add("Format", summaryInformation.Format);
                Properties.Add("Keywords", summaryInformation.Keywords);
                Properties.Add("LastAuthor", summaryInformation.LastAuthor);
                Properties.Add("LastPrinted", summaryInformation.LastPrinted);
                Properties.Add("LastSaveDateTime", summaryInformation.LastSaveDateTime);
                Properties.Add("OSVersion", summaryInformation.OSVersion);
                Properties.Add("PageCount", summaryInformation.PageCount);
                Properties.Add("RevNumber", summaryInformation.RevNumber);
                Properties.Add("SectionCount", summaryInformation.SectionCount);
                Properties.Add("Security", summaryInformation.Security);
                Subject = summaryInformation.Subject;
                Properties.Add("Template", summaryInformation.Template);
                Title = summaryInformation.Title;
                Properties.Add("WordCount", summaryInformation.WordCount);
                document.Close();

            }
        }
    }
    class DocAnalysis : OfficeAnalysis
    {


        public DocAnalysis(string extension, Stream stream) : base(extension, stream)
        {
            OffileFileType = OffileFileType.DOC;
        }

        public override void Handle()
        {
            var iszip = SharpCompress.Archives.Zip.ZipArchive.IsZipFile(IOStream);
            IOStream.Seek(0, SeekOrigin.Begin);
            if (iszip)
            {

                var document = new NPOI.XWPF.UserModel.XWPFDocument(IOStream);
                var properties = document.GetProperties();
                var coreproperties = properties.CoreProperties;
                Properties.Add("Created", coreproperties.Created);
                Properties.Add("Category", coreproperties.Category);
                Properties.Add("ContentType", coreproperties.ContentType);
                Author = coreproperties.Creator;
                Properties.Add("Description", coreproperties.Description);
                Properties.Add("Identifier", coreproperties.Identifier);
                Keywords = coreproperties.Keywords;
                Properties.Add("LastPrinted", coreproperties.LastPrinted);
                Properties.Add("Modified", coreproperties.Modified);
                Properties.Add("Revision", coreproperties.Revision);
                Title = coreproperties.Title;
                Subject = coreproperties.Subject;
                var UnderlyingProperties = properties.CoreProperties.GetUnderlyingProperties();

                Properties.Add("Size", UnderlyingProperties.Size);

                var extendedProperties = properties.ExtendedProperties;
                Properties.Add("ApplicationName", extendedProperties.Application);
                Properties.Add("AppVersion", extendedProperties.AppVersion);
                Properties.Add("Characters", extendedProperties.Characters);
                Properties.Add("CharactersWithSpaces", extendedProperties.CharactersWithSpaces);
                Properties.Add("Company", extendedProperties.Company);
                Properties.Add("HiddenSlides", extendedProperties.HiddenSlides);
                Properties.Add("HyperlinkBase", extendedProperties.HyperlinkBase);
                Properties.Add("Lines", extendedProperties.Lines);
                Properties.Add("Manager", extendedProperties.Manager);
                Properties.Add("MMClips", extendedProperties.MMClips);
                Properties.Add("Notes", extendedProperties.Notes);
                Properties.Add("Pages", extendedProperties.Pages);
                Properties.Add("Paragraphs", extendedProperties.Paragraphs);
                Properties.Add("PresentationFormat", extendedProperties.PresentationFormat);
                Properties.Add("Slides", extendedProperties.Slides);
                Properties.Add("Template", extendedProperties.Template);
                Properties.Add("TotalTime", extendedProperties.TotalTime);
                Properties.Add("Words", extendedProperties.Words);
                XWPFWordExtractor ex = new XWPFWordExtractor(document);
                Content = ex.Text;
                document.Close();


            }
            else
            {


                var ex = new NPOI.HWPF.Extractor.WordExtractor(IOStream);
                var summaryInformation = ex.SummaryInformation;
                Properties.Add("ApplicationName", summaryInformation.ApplicationName);
                Author = summaryInformation.Author;
                Properties.Add("ByteOrder", summaryInformation.ByteOrder);
                Properties.Add("CharCount", summaryInformation.CharCount);
                Properties.Add("Comments", summaryInformation.Comments);
                Properties.Add("CreateDateTime", summaryInformation.CreateDateTime);
                Properties.Add("EditTime", summaryInformation.EditTime);
                Properties.Add("Format", summaryInformation.Format);
                Keywords = summaryInformation.Keywords;
                Properties.Add("LastAuthor", summaryInformation.LastAuthor);
                Properties.Add("LastPrinted", summaryInformation.LastPrinted);
                Properties.Add("LastSaveDateTime", summaryInformation.LastSaveDateTime);
                Properties.Add("OSVersion", summaryInformation.OSVersion);
                Properties.Add("PageCount", summaryInformation.PageCount);
                Properties.Add("RevNumber", summaryInformation.RevNumber);
                Properties.Add("SectionCount", summaryInformation.SectionCount);
                Properties.Add("Security", summaryInformation.Security);
                Subject = summaryInformation.Subject;
                Properties.Add("Template", summaryInformation.Template);
                Title = summaryInformation.Title;
                Properties.Add("WordCount", summaryInformation.WordCount);
                Content = ex.Text;



            }
        }
    }
    class PptAnalysis : OfficeAnalysis
    {
        public PptAnalysis(string extension, Stream stream) : base(extension, stream)
        {
            OffileFileType = OffileFileType.PPT;
        }
        void ReaderContent(OpenXmlElement element, StringBuilder builder)
        {
            if (element is DocumentFormat.OpenXml.Drawing.Paragraph)
            {
                builder.AppendLine(element.InnerText);
                return;
            } 
            foreach (var item in element.ChildElements)
            {
                ReaderContent(item, builder);
            }
        }
        public override void Handle()
        {
            var packing = System.IO.Packaging.Package.Open(IOStream);
            PresentationDocument document = PresentationDocument.Open(packing);
            var list = document.PresentationPart.SlideParts;
            StringBuilder builder = new StringBuilder();
            foreach (var item in list)
            {
                foreach (var element in item.Slide.ChildElements)
                {
                    ReaderContent(element, builder);
             
                }
            }
            var props = document.PackageProperties;
            Author = props.Creator;
            Keywords = props.Keywords;
            Subject = props.Subject;
            Title = props.Title;
            Properties.Add("ContentStatus", props.ContentStatus);
            Properties.Add("Category", props.Category);
            Properties.Add("Language", props.Language);
            Properties.Add("Created", props.Created);
            Properties.Add("Description", props.Description);
            Properties.Add("Identifier", props.Identifier);
            Properties.Add("LastModifiedBy", props.LastModifiedBy);
            Properties.Add("LastPrinted", props.LastPrinted);
            Properties.Add("Modified", props.Modified);
            Properties.Add("Revision", props.Revision);
            Properties.Add("Version", props.Version);
            Content = builder.ToString();

        }
    }
    class PdfAnalysis : OfficeAnalysis
    {
        public PdfAnalysis(string extension, Stream stream) : base(extension, stream)
        {
            OffileFileType = OffileFileType.PDF;
        }
        public override void Handle()
        {

            iText.Kernel.Pdf.PdfReader pdfReader = new iText.Kernel.Pdf.PdfReader(IOStream);
            iText.Kernel.Pdf.PdfDocument document = new iText.Kernel.Pdf.PdfDocument(pdfReader);

            var pagesize = document.GetDefaultPageSize();

            var documentInfo = document.GetDocumentInfo();
            var pdfVersion = document.GetPdfVersion();
            var associatedFiles = document.GetAssociatedFiles();
            var pages = document.GetNumberOfPages();

            StringBuilder builder = new StringBuilder();
            for (int i = 1; i <= pages; i++)
            {
                var pdfpage = document.GetPage(i);
                var text = iText.Kernel.Pdf.Canvas.Parser.PdfTextExtractor.GetTextFromPage(pdfpage);
                builder.AppendLine(text);
            }
            Author = documentInfo.GetAuthor();
            Properties.Add("Creator", documentInfo.GetCreator());
            Keywords = documentInfo.GetKeywords();
            Properties.Add("Producer", documentInfo.GetProducer());
            Subject = documentInfo.GetSubject();
            Title = documentInfo.GetTitle();

            Content = builder.ToString();
        }
    }
}
