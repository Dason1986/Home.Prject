using System.ComponentModel.DataAnnotations;
using Library.ComponentModel.Model;
using Library.Domain;

namespace Home.DomainModel.Aggregates.AlertAgg
{
    [System.ComponentModel.Description("手機短信記錄"),
     System.ComponentModel.DisplayName("手機短信記錄")]
    public class PhoneMessageLogEntity : MessageLogEntity
    {
        public PhoneMessageLogEntity(ICreatedInfo crate) : base(crate)
        {
        }

        public PhoneMessageLogEntity()
        {
        }

        [System.ComponentModel.Description("電話號碼"),
         System.ComponentModel.DisplayName("電話號碼")]
        [StringLength(50), Required]
        public string PhoneNumber { get; set; }
    }
}