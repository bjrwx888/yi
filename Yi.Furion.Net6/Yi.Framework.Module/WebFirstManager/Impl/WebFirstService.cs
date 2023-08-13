using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Yi.Framework.Infrastructure.Ddd.Repositories;
using Yi.Framework.Infrastructure.Ddd.Services;
using Yi.Framework.Module.WebFirstManager.Dtos;
using Yi.Framework.Module.WebFirstManager.Entities;

namespace Yi.Framework.Module.WebFirstManager.Impl
{
    public class WebFirstService : ApplicationService, IWebFirstService
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
