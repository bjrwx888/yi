using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Yi.Framework.ChatHub.Domain.Shared.Enums;

namespace Yi.Framework.ChatHub.Domain.Shared.Model
{
    public class MessageContext
    {
        public static MessageContext CreatePersonal(string content, Guid userId,Guid sendUserId)
        {
            return new MessageContext() { MessageType = MessageTypeEnum.Personal, Content = content, ReceiveId = userId ,SendUserId= sendUserId };
        }

        public static MessageContext CreateAll(string content, Guid sendUserId)
        {
            return new MessageContext() { MessageType = MessageTypeEnum.All, Content = content, SendUserId = sendUserId };
        }


        /// <summary>
        /// 消息类型
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MessageTypeEnum MessageType { get; set; }
        /// <summary>
        /// 接收者(用户id、群组id)
        /// </summary>
        public Guid? ReceiveId { get; set; }

        /// <summary>
        /// 发送者的用户id
        /// </summary>
        public Guid SendUserId { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }
        public DateTime CreationTime { get;protected set; }=DateTime.Now;
    }


}
