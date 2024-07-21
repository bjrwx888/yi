using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Services;
using Yi.Framework.ChatHub.Domain.Managers;
using Yi.Framework.ChatHub.Domain.Shared.Dtos;
using Yi.Framework.ChatHub.Domain.Shared.Model;

namespace Yi.Framework.ChatHub.Application.Services
{
    public class AiChatService : ApplicationService
    {
        private readonly AiManager _aiManager;
        private readonly UserMessageManager _userMessageManager;
        public AiChatService(AiManager aiManager, UserMessageManager userMessageManager) { _aiManager = aiManager; _userMessageManager = userMessageManager; }


        /// <summary>
        /// ai聊天
        /// </summary>
        /// <param name="chatContext"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]

        public async Task ChatAsync([FromBody] List<AiChatContextDto> chatContext)
        {
            var contextId = Guid.NewGuid();
            await foreach (var aiResult in _aiManager.ChatAsStreamAsync(chatContext))
            {
                await _userMessageManager.SendMessageAsync(MessageContext.CreateAi(aiResult, CurrentUser.Id!.Value, contextId));
            }

            await _userMessageManager.SendMessageAsync(MessageContext.CreateAi(null, CurrentUser.Id!.Value, contextId));
        }
    }
}
