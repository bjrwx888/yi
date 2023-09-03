using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Module.WeChat.Token
{
    public interface IWeChatToken
    {
        public Task<string> GetTokenAsync();
    }

}
