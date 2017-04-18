using System.ComponentModel.DataAnnotations;
using Library.ComponentModel.Model;
using Library.Domain;

namespace Home.DomainModel.Aggregates.AlertAgg
{
    [System.ComponentModel.Description("郵件記錄"),
         System.ComponentModel.DisplayName("郵件記錄")]
    public class MailMessageLogEntity : MessageLogEntity
    {
        public MailMessageLogEntity(ICreatedInfo crate) : base(crate)
        {
        }

        public MailMessageLogEntity()
        {
        }

        private string _to;
        private string _cc;

        /// <summary>
        ///
        /// </summary>
        [Required, StringLength(255)]
        [System.ComponentModel.Description("發送"),
     System.ComponentModel.DisplayName("發送")]
        public string To
        {
            get { return _to; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    MailUtility.IsEmail(value);
                _to = value;
            }
        }

        /// <summary>
        ///
        /// </summary>

        [System.ComponentModel.Description("抄送"),
     System.ComponentModel.DisplayName("抄送")]
        [StringLength(255)]
        public string Cc
        {
            get { return _cc; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    MailUtility.IsEmail(value);
                _cc = value;
            }
        }
    }
}