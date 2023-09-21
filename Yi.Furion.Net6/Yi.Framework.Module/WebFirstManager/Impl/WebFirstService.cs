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
using Yi.Framework.Module.WebFirstManager.Domain;
using Yi.Framework.Module.WebFirstManager.Dtos.WebFirst;
using Yi.Framework.Module.WebFirstManager.Entities;

namespace Yi.Framework.Module.WebFirstManager.Impl
{
    [ApiDescriptionSettings("WebFirstManager")]
    public class WebFirstService : ApplicationService, IWebFirstService, IDynamicApiController, ITransient
    {
        private IRepository<TableEntity> _tableRepository;
        private TemplateManager _templateManager;
        public WebFirstService(IRepository<TableEntity> tableRepository, TemplateManager templateManager)
        {
            _tableRepository = tableRepository;
            _templateManager = templateManager;
        }

        /// <summary>
        /// 一键构建生成代码
        /// </summary>
        /// <returns></returns>
        public async Task PostBuildCodeAsync()
        {
            //获取全部表
            var tables = await _tableRepository.GetListAsync();
            foreach (var table in tables)
            {
                await BuildSingleTableAsync(table);
            }

        }


        /// <summary>
        /// 一键构建生成表
        /// </summary>
        /// <returns></returns>
        public async Task PostBuildTableAsync()
        {
        }







        /// <summary>
        /// 每次去构建一张表的数据
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private async Task BuildSingleTableAsync(TableEntity table)
        {
            await _templateManager.HandlerAsync(table);
        }
    }
}
