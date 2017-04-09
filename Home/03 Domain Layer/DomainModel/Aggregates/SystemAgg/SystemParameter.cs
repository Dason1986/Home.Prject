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
    [System.ComponentModel.Description("系統參數設置"),
           System.ComponentModel.DisplayName("系統參數設置")]
    public class SystemParameter : AuditedEntity, IParameter
    {
        public SystemParameter()
        {

        }
        public SystemParameter(ICreatedInfo createinfo) : base(createinfo)
        {

        }
        [StringLength(255)]
        [System.ComponentModel.Description("參數說明"),
          System.ComponentModel.DisplayName("參數說明")]
        public string Description { get; set; }
        [StringLength(50)]
        [System.ComponentModel.Description("參數分類"),
          System.ComponentModel.DisplayName("參數分類")]
        public string ParameterGroup { get; set; }
        [System.ComponentModel.Description("參數只讀"),
        System.ComponentModel.DisplayName("參數只讀")]
        public bool IsReadOnly { get; set; }
        [StringLength(50)]
        [System.ComponentModel.Description("參數主鍵"),
          System.ComponentModel.DisplayName("參數主鍵")]
        public string ParameterKey { get; set; }
        [StringLength(255)]
        [System.ComponentModel.Description("參數值"),
          System.ComponentModel.DisplayName("參數值")]
        public string ParameterValue { get; set; }

        string IParameter.Group
        {
            get
            {
                return this.ParameterGroup;
            }
        }

       

        string IParameter.Key
        {
            get
            {
                return this.ParameterKey;
            }
        }

        

        string IParameter.Value
        {
            get
            {
                return this.ParameterValue;
            }
        }

        void IParameter.SetValue(string value)
        {
            if (IsReadOnly) throw new LogicException("不能修改只讀的設置");
            ParameterValue = value;
        }
    }
}