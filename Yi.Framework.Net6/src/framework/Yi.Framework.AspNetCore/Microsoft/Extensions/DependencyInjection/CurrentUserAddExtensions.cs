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

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CurrentUserAddExtensions
    {
        public static IServiceCollection AddCurrentUserServer(this IServiceCollection services)
        {
            return services.AddScoped<ICurrentUser, CurrentUser>();
        }


     
    }



}
