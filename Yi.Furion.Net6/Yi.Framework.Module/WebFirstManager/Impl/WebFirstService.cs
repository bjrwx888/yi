using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furion.DatabaseAccessor;
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
        private IRepository<TableAggregateRoot> _tableRepository;
        private CodeFileManager _codeFileManager;
        private WebTemplateManager _webTemplateManager;
        public WebFirstService(IRepository<TableAggregateRoot> tableRepository, CodeFileManager codeFileManager, WebTemplateManager webTemplateManager)
        {
            _tableRepository = tableRepository;
            _codeFileManager = codeFileManager;
            _webTemplateManager= webTemplateManager;
        }

        /// <summary>
        /// Web To Code
        /// </summary>
        /// <returns></returns>
        public async Task PostWebBuildCodeAsync()
        {
            //获取全部表
            var tables = await _tableRepository.GetListAsync();
            foreach (var table in tables)
            {
                await _codeFileManager.BuildWebToCodeAsync(table);
            }

        }


        /// <summary>
        /// Web To Db
        /// </summary>
        /// <returns></returns>
        public async Task PostWebBuildDbAsync()
        {
        }

        /// <summary>
        /// Code To Web
        /// </summary>
        /// <returns></returns>
        [UnitOfWork]
        public async Task PostCodeBuildWebAsync()
        {
            var tableAggregateRoots =await  _webTemplateManager.BuildCodeToWebAsync();
            //覆盖数据库，将聚合根保存到数据库
            _tableRepository._Db.DbMaintenance.TruncateTable<TableAggregateRoot>();
            _tableRepository._Db.DbMaintenance.TruncateTable<FieldEntity>();

            //导航插入即可
            await _tableRepository._Db.InsertNav(tableAggregateRoots).Include(x => x.Fields).ExecuteCommandAsync();

         
        }


        /// <summary>
        /// Code To Db
        /// </summary>
        /// <returns></returns>
        public async Task PostCodeBuildDbAsync()
        {
        }
    }
}
