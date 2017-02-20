using Library;
using Library.ComponentModel.Model;
using Library.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home.DomainModel.Aggregates.SystemAgg
{
    public class SystemParameter : AuditedEntity, IParameter
    {
        public SystemParameter()
        {

        }
        public SystemParameter(ICreatedInfo createinfo) : base(createinfo)
        {

        }
        [StringLength(255)]
        public string Description { get; set; }
        [StringLength(50)]
        public string Group { get; set; }

        public bool IsReadOnly { get; set; }
        [StringLength(50)]
        public string Key { get; set; }
        [StringLength(255)]
        public string Value { get; set; }

        void IParameter.SetValue(string value)
        {
            if (IsReadOnly) throw new LogicException("不能修改只讀的設置");
            Value = value;
        }
    }
}