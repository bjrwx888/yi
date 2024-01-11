using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Services;
using Volo.Abp.Users;
using Yi.Framework.Bbs.Domain.Managers;

namespace Yi.Framework.Bbs.Application.Services.Integral
{
    public class IntegralService : ApplicationService
    {
        private IntegralManager _integralManager;
        private ICurrentUser _currentUser;
        public IntegralService(IntegralManager integralManager, ICurrentUser currentUser)
        {
            _integralManager = integralManager;
            _currentUser = currentUser;
        }


        /// <summary>
        /// 签到
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<object> PostSignInAsync()
        {
            var value = await _integralManager.SignInAsync(_currentUser.Id ?? Guid.Empty);
            return new { value };
        }
    }
}
