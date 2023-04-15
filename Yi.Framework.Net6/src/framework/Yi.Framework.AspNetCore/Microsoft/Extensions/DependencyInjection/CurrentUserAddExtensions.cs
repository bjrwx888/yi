using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Const;
using Yi.Framework.Core.CurrentUsers;
using Yi.Framework.Core.CurrentUsers.Accessor;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CurrentUserAddExtensions
    {
        public static IServiceCollection AddCurrentUserServer(this IServiceCollection services)
        {
            services.AddSingleton<ICurrentPrincipalAccessor, HttpContextCurrentPrincipalAccessor>();
            return services.AddTransient<ICurrentUser, CurrentUser>();
        }


     
    }



}
