using Library.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home.DomainModel.Aggregates.FileAgg
{
    [DisplayName("文件邏輯樹"), Description("文件邏輯樹")]
    public class FileLogicTree : Entity
    {
        [DisplayName("文件邏輯樹"), Description("文件邏輯樹")]
        public Guid ParentId { get; set; }
        public virtual FileLogicTree Parent { get; set; }
        [DisplayName("下級"), Description("下級")]
        public virtual ICollection<FileLogicTree> Childrens { get; private set; }
        [DisplayName("名稱"), Description("名稱")]
        public string Name { get; set; }
        [DisplayName("節點類型"), Description("節點類型")]
        public FileLogicType NodeType { get; set; }
        [DisplayName("文件編號"), Description("文件編號")]
        public Guid FileId { get; set; }
        public virtual FileInfo File { get; set; }
        public void AddChild(FileLogicTree child) {
            if (child == null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            if (Childrens == null) Childrens = new List<FileLogicTree>();
            Childrens.Add(child);
        }
    }
}
