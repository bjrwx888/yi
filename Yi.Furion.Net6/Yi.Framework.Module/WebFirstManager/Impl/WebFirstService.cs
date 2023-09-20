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
    public class WebFirstService : ApplicationService, IWebFirstService, IDynamicApiController, ITransient
    {
        private IRepository<TemplateEntity> _repository;
        private IRepository<TemplateVarEntity> _varRepository;
        public WebFirstService(IRepository<TemplateEntity> repository, IRepository<TemplateVarEntity> varRepository)
        {
            _repository = repository;
            _varRepository = varRepository;
        }

        /// <summary>
        /// 一键构建
        /// </summary>
        /// <returns></returns>
        public async Task PostBuildAsync()
        {
            //获取全部模板
            var templates = await _repository.GetListAsync();
            var varTemps = await _varRepository.GetListAsync();

        }

        private async Task BuildSingleAsync(TemplateEntity template, List<TemplateVarEntity> templateVars)
        {
            foreach (var tempVar in templateVars)
                template.TemplateStr.Replace(tempVar.Value, "model");




        }
    }
}
