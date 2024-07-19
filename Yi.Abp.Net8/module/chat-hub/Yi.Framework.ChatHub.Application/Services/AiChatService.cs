using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Volo.Abp.Application.Services;
using Yi.Framework.ChatHub.Domain.Managers;
using Yi.Framework.ChatHub.Domain.Shared.Model;
using Yi.Framework.ChatHub.Domain.SignalRHubs;

namespace Yi.Framework.ChatHub.Application.Services
{
    public class AiChatService : ApplicationService
    {
        private readonly AiManager _aiManager;
        private readonly UserMessageManager _userMessageManager;
        public AiChatService(AiManager aiManager, UserMessageManager userMessageManager) { _aiManager = aiManager; _userMessageManager = userMessageManager; }

        [Authorize]
        [HttpPost]
        public async Task ChatAsync()
        {
            await foreach (var aiResult in _aiManager.ChatAsStreamAsync())
            {
                await _userMessageManager.SendMessageAsync(MessageContext.CreateAi(aiResult, CurrentUser.Id!.Value));
            }
        }
    }
}
