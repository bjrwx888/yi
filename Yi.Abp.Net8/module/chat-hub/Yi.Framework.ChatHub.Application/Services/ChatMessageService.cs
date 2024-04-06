using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Services;
using Yi.Framework.ChatHub.Application.Contracts.Dtos;
using Yi.Framework.ChatHub.Domain.Managers;
using Yi.Framework.ChatHub.Domain.Shared.Model;

namespace Yi.Framework.ChatHub.Application.Services
{
    public class ChatMessageService : ApplicationService
    {
        private UserMessageManager _userMessageManager;
        public ChatMessageService(UserMessageManager userMessageManager) { _userMessageManager = userMessageManager; }
        /// <summary>
        /// 发送个人消息
        /// </summary>
        /// <returns></returns>
        [HttpPost("chat-message/personal")]
        [Authorize]
        public async Task SendPersonalMessageAsync(PersonalMessageInputDto input)
        {
            await _userMessageManager.SendMessageAsync(MessageContext.CreatePersonal(input.Content, input.UserId,CurrentUser.Id!.Value)); ;
        }


        /// <summary>
        /// 发送群组消息
        /// </summary>
        /// <returns></returns>
        [HttpPost("chat-message/group")]
        [Authorize]
        public async Task SendGroupMessageAsync(GroupMessageInputDto input)
        {
            await _userMessageManager.SendMessageAsync(MessageContext.CreateAll(input.Content, CurrentUser.Id!.Value)); ;

        }
    }
}
