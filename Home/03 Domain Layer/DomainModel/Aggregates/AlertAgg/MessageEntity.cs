using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Library.ComponentModel.Model;
using Library.Domain;

namespace Home.DomainModel.Aggregates.AlertAgg
{
    [System.ComponentModel.Description("消息類"),
         System.ComponentModel.DisplayName("消息類")]
    public class MessageEntity : AuditedEntity
    {
        public MessageEntity(ICreatedInfo crate) : base(crate)
        {
        }

        public MessageEntity()
        {
        }

        /// <summary>
        ///
        /// </summary>
        [System.ComponentModel.Description("消息主題"),
      System.ComponentModel.DisplayName("消息主題")]
        [Required]
        [StringLength(255)]
        public string Subject { get; set; }

        /// <summary>
        ///
        /// </summary>
        [System.ComponentModel.Description("消息內容"),
      System.ComponentModel.DisplayName("消息內容")]
        [Required]
        public string Content { get; set; }

        [System.ComponentModel.Description("記錄"),
System.ComponentModel.DisplayName("記錄")]
        public virtual ICollection<MessageLogEntity> Logs { get; set; }

        //        [System.ComponentModel.Description("記錄"),
        //        System.ComponentModel.DisplayName("記錄")]
        //        public virtual ICollection<MessageLogEntity> PhoneLogs { get; set; }

        //        [System.ComponentModel.Description("記錄"),
        //System.ComponentModel.DisplayName("記錄")]
        //        public virtual ICollection<MessageLogEntity> MailLogs { get; set; }
    }
}