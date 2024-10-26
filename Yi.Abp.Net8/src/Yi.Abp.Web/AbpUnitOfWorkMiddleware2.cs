// Decompiled with JetBrains decompiler
// Type: Volo.Abp.AspNetCore.Uow.AbpUnitOfWorkMiddleware
// Assembly: Volo.Abp.AspNetCore, Version=8.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E24BDAEE-E92D-4420-84F7-3DD088C817A4
// Assembly location: C:\Users\Administrator\.nuget\packages\volo.abp.aspnetcore\8.2.0\lib\net8.0\Volo.Abp.AspNetCore.dll
// XML documentation location: C:\Users\Administrator\.nuget\packages\volo.abp.aspnetcore\8.2.0\lib\net8.0\Volo.Abp.AspNetCore.xml

using Microsoft.AspNetCore.Components.Endpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Middleware;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

#nullable enable
namespace Volo.Abp.AspNetCore.Uow
{
    public class AbpUnitOfWorkMiddleware2 : AbpMiddlewareBase, ITransientDependency
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly AbpAspNetCoreUnitOfWorkOptions _options;

        public AbpUnitOfWorkMiddleware2(
            IUnitOfWorkManager unitOfWorkManager,
            IOptions<AbpAspNetCoreUnitOfWorkOptions> options)
        {
            this._unitOfWorkManager = unitOfWorkManager;
            this._options = options.Value;
        }

        public override async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // await next(context);
            // await next(context).ConfigureAwait(false);
            if (await ShouldSkipAsync(context, next).ConfigureAwait(false) || IsIgnoredUrl(context))
            {
                await next(context).ConfigureAwait(false);
            }
            else
            {
                using (IUnitOfWork uow = _unitOfWorkManager.Reserve("_AbpActionUnitOfWork"))
                {
                    await next(context).ConfigureAwait(false);
                    await uow.CompleteAsync(context.RequestAborted).ConfigureAwait(false);
                }
            }
        }

        private bool IsIgnoredUrl(HttpContext context)
        {
            return context.Request.Path.Value != null && this._options.IgnoredUrls.Any<string>(
                (Func<string, bool>)(x =>
                    context.Request.Path.Value.StartsWith(x, StringComparison.OrdinalIgnoreCase)));
        }

        protected override async Task<bool> ShouldSkipAsync(HttpContext context, RequestDelegate next)
        {
            return context.GetEndpoint()?.Metadata?.GetMetadata<RootComponentMetadata>() != null ||
                   await base.ShouldSkipAsync(context, next).ConfigureAwait(false);
        }
    }
}