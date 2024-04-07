using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Services;
using Volo.Abp.EventBus.Local;
using Yi.Framework.Bbs.Domain.Shared.Etos;
using Yi.Framework.ChatHub.Application.Contracts.Dtos;
using Yi.Framework.ChatHub.Domain.Managers;
using Yi.Framework.ChatHub.Domain.Shared.Enums;
using Yi.Framework.ChatHub.Domain.Shared.Model;

namespace Yi.Framework.ChatHub.Application.Services
{
    public class ChatMessageService : ApplicationService
    {
        private UserMessageManager _userMessageManager;
        private ILocalEventBus _localEventBus;
        public ChatMessageService(UserMessageManager userMessageManager, ILocalEventBus localEventBus) { _userMessageManager = userMessageManager; _localEventBus = localEventBus; }
        /// <summary>
        /// 发送个人消息
        /// </summary>
        /// <returns></returns>
        [HttpPost("chat-message/personal")]
        [Authorize]
        public async Task SendPersonalMessageAsync(PersonalMessageInputDto input)
        {
            var mesageContext = MessageContext.CreatePersonal(input.Content, input.UserId, CurrentUser.Id!.Value);
            await _userMessageManager.SendMessageAsync(mesageContext);

            await _userMessageManager.CreateMessageStoreAsync(mesageContext);
        }


        /// <summary>
        /// 发送群组消息
        /// </summary>
        /// <returns></returns>
        [HttpPost("chat-message/group")]
        [Authorize]
        public async Task SendGroupMessageAsync(GroupMessageInputDto input)
        {
            //领域调用，群主消息调用bbs领域

            //如果钱钱不足，将自动断言
            await _localEventBus.PublishAsync<MoneyChangeEventArgs>(new MoneyChangeEventArgs { UserId = CurrentUser.Id.Value, Number = -1 });

            var mesageContext = MessageContext.CreateAll(input.Content, CurrentUser.Id!.Value);
            await _userMessageManager.SendMessageAsync(mesageContext);

            await _userMessageManager.CreateMessageStoreAsync(mesageContext);
        }

        /// <summary>
        /// 查询消息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize]
        [RemoteService(IsEnabled = false)]
        public async Task<List<MessageContext>> GetListAsync(ChatMessageGetListInput input)
        {
            var entities = await _userMessageManager._repository._DbQueryable
                 .WhereIF(input.MessageType is not null, x => x.MessageType == input.MessageType)
                 .WhereIF(input.ReceiveId is not null, x => x.ReceiveId == input.ReceiveId)
                 .WhereIF(input.SendUserId is not null, x => x.SendUserId == input.SendUserId)
                 .WhereIF(input.StartTime is not null && input.EndTime is not null, x => x.CreationTime >= input.StartTime && x.CreationTime <= input.EndTime)
                 .OrderBy(x => x.CreationTime)
                 .ToListAsync();
            var output = entities.Adapt<List<MessageContext>>();
            return output;
        }

        /// <summary>
        /// 获取用户自己的消息
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("chat-message/account")]
        public async Task<List<MessageContext>> GetAccountMessageListAsync()
        {
            //默认显示1个月的个人数据
            var userId = CurrentUser.Id!.Value;
            //3个类型数据
            var entities = await _userMessageManager._repository._DbQueryable
                .Where(x => x.MessageType == MessageTypeEnum.All
                || x.ReceiveId == userId
                || x.SendUserId == userId
                )
                 .Where(x => x.CreationTime >= DateTime.Now.AddDays(-30))
                  .OrderBy(x => x.CreationTime)
                 .ToListAsync();
            var output = entities.Adapt<List<MessageContext>>();
            return output;
        }
    }
}
