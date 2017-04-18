using System;
using Library.ComponentModel.Model;
using Library.Domain;

namespace Home.DomainModel.Aggregates.AlertAgg
{
    [System.ComponentModel.Description("消息記錄"),
     System.ComponentModel.DisplayName("消息記錄")]
    public abstract class MessageLogEntity : AuditedEntity
    {
        public MessageLogEntity(ICreatedInfo crate) : base(crate)
        {
        }

        public MessageLogEntity()
        {
        }

        [System.ComponentModel.Description("消息狀態"),
        System.ComponentModel.DisplayName("消息狀態")]
        public MessageState State { get; set; }

        [System.ComponentModel.Description("消息編號"),
     System.ComponentModel.DisplayName("消息編號")]
        public Guid MessageEntityID { get; set; }

        [System.ComponentModel.Description("消息"),
     System.ComponentModel.DisplayName("消息")]
        public virtual MessageEntity Message { get; set; }
    }
}