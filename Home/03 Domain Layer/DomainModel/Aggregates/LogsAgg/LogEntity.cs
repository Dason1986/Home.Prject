using Library.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home.DomainModel.Aggregates.LogsAgg
{
    [DisplayName("出錯Logger工具日誌"), Description("出錯Logger工具日誌")]
    public class LogEntity : Entity
    {
        [DisplayName("調用方法"), Description("調用方法")]
        public string CallSite { get; set; }
        [DisplayName("日期"), Description("日期")]
        public string Date { get; set; }
        [DisplayName("異常"), Description("異常")]
        public string Exception { get; set; }
        [DisplayName("等級"), Description("等級")]
        public string Level { get; set; }
        [DisplayName("日誌名"), Description("日誌名")]
        public string Logger { get; set; }
        [DisplayName("機器"), Description("機器")]
        public string MachineName { get; set; }
        [DisplayName("信息"), Description("調用栈")]
        public string Message { get; set; }
        [DisplayName("調用栈"), Description("調用栈")]
        public string StackTrace { get; set; }
        [DisplayName("線程"), Description("線程")]
        public string Thread { get; set; }
        [DisplayName("用戶名"), Description("用戶名")]
        public string Username { get; set; }
    }
}
