using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Mapster;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Yi.Framework.Infrastructure.Ddd.Repositories;
using Yi.Framework.Infrastructure.Ddd.Services;
using Yi.Framework.Module.WebFirstManager.Dtos.WebFirst;
using Yi.Framework.Module.WebFirstManager.Entities;

namespace Yi.Framework.Module.WebFirstManager.Impl
{
    [ApiDescriptionSettings("WebFirstManager")]
    public class WebFirstService : ApplicationService, IWebFirstService,IDynamicApiController,ITransient
    {
        private IRepository<TemplateEntity> _repository;
        public WebFirstService(IRepository<TemplateEntity> repository) { _repository = repository; }

        /// <summary>
        /// 根据模板id生成对应的结果
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<WebFirstGetOutputDto> GetAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);

            return entity.Adapt<WebFirstGetOutputDto>();
        }

    }
}
