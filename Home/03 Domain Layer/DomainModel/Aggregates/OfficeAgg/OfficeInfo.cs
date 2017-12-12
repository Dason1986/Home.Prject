using Library.Domain;
using System.ComponentModel.DataAnnotations;

namespace Home.DomainModel.Aggregates.OfficeAgg
{
    public class OfficeInfo
    {
        [StringLength(50)]
        [System.ComponentModel.Description("作者"),
          System.ComponentModel.DisplayName("作者")]
        public string Author { get; set; }

        [StringLength(50)]
        [System.ComponentModel.Description("主題"),
          System.ComponentModel.DisplayName("主題")]
        public string Subject { get; set; }

        [StringLength(50)]
        [System.ComponentModel.Description("標題"),
          System.ComponentModel.DisplayName("標題")]
        public string Title { get; set; }

        [StringLength(255)]
        [System.ComponentModel.Description("關鍵字"),
          System.ComponentModel.DisplayName("關鍵字")]
        public string Keyworks { get; set; }

        [System.ComponentModel.Description("文本内容"),
          System.ComponentModel.DisplayName("文本内容")]
        public string Content { get; set; }

        public void Substring()
        {
            Author= Substring(Author, 50);
            Subject= Substring(Subject, 50);
            Title= Substring(Title, 50);
            Keyworks= Substring(Keyworks, 255);
        }
        private string Substring(  string str, int length)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > length)
            {
                str = str.Substring(0, (length - 2)) + "……";
            }
            return str;
        }
    }
}