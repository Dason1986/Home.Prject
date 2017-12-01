using Library.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home.DomainModel.Aggregates.LogsAgg
{
    [DisplayName("服務隊列日誌"), Description("服務隊列日誌")]
    public class DomainEventArgsLog : Entity
    {
        [DisplayName("是否已經執行"), Description("是否已經執行")]
        public bool IsExecuted { get; set; }
        [DisplayName("是否有錯"), Description("是否有錯")]
        public bool HasError { get; set; }
        [DisplayName("錯誤信息"), Description("錯誤信息")]
        public string ErrorMsg { get; set; }
        [DisplayName("參數信息類型"), Description("參數信息類型")]
        public string DomainEventType { get; set; }
        [DisplayName("參數信息"), Description("參數信息")]
        public string DomainEvent { get; set; }
    }
}
