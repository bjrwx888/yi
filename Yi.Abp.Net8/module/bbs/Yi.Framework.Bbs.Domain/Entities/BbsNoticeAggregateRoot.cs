namespace Yi.Framework.Bbs.Domain.Entities
{
    public class BbsNoticeAggregateRoot
    {
        public Guid AcceptUserId { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        public MessageTypeEnum MessageType { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// 已读时间
        /// </summary>
        public DateTime? ReadTime { get; set; }
    }

    /// <summary>
    /// 消息类型
    /// </summary>
    public enum MessageTypeEnum
    {
        
    }
}
