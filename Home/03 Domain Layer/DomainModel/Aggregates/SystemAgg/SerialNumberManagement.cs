using Library.ComponentModel.Model;
using Library.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home.DomainModel.Aggregates.SystemAgg
{
    /// <summary>
    /// 序列號管理
    /// </summary>
    [DisplayName("序列號管理"), Description("序列號管理")]
    public class SerialNumberManagement : Entity
    {
        public SerialNumberManagement(ICreatedInfo clientHeaderManaegment)
            : base(clientHeaderManaegment)
        {

        }

        public SerialNumberManagement()
        {

        }
        /// <summary>
        /// 代碼
        /// </summary>
        [Description(@"代碼")]
        public string Code { get; set; }

        /// <summary>
        /// 當前號碼
        /// </summary>
        [Description(@"當前號碼")]
        public int CurrentNumber { get; set; }

        /// <summary>
        /// 生成格式 ZN-{Date:yyyy}-{Number:8}   ZN-2015-00000324
        /// </summary>
        [Description(@"生成格式")]
        public string SerialNumberFormat { get; set; }

        /// <summary>
        /// 是否自定義
        /// </summary>
        [Description(@"是否自定義")]
        public bool IsCustom { get; set; }

        /// <summary>
        /// 自定義生成Class
        /// </summary>
        [Description(@"自定義生成Class")]
        public string CustomClass { get; set; }
    }
}
