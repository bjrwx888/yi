using Microsoft.Extensions.DependencyInjection;
using Yi.Framework.Infrastructure.CurrentUsers;
using Yi.Framework.Infrastructure.CurrentUsers.Accessor;

namespace Yi.Framework.Infrastructure.AspNetCore
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
