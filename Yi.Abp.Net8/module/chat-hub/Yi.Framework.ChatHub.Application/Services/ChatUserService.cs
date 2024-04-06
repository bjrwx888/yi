using Mapster;
using Volo.Abp.Application.Services;
using Yi.Framework.ChatHub.Domain.Managers;
using Yi.Framework.ChatHub.Domain.Shared.Model;

namespace Yi.Framework.ChatHub.Application.Services
{
    public class ChatUserService : ApplicationService
    {
        private UserMessageManager _messageManager;
        public ChatUserService(UserMessageManager messageManager) { _messageManager = messageManager; }

        public async Task<List<ChatUserModel>> GetListAsync()
        {
            var userList = await _messageManager.GetAllUserAsync();
            var output = userList.Adapt<List<ChatUserModel>>();
            return output;
        }
    }
}
