using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furion.Schedule;
using Microsoft.Extensions.Logging;
using SqlSugar;
using Yi.Framework.Infrastructure.Data.DataSeeds;
using Yi.Framework.Module.DictionaryManager.Entities;
using Yi.Furion.Core.Rbac.Entities;

namespace Yi.Furion.Application.Rbac.Job
{
    public class SystemDataJob : IJob
    {
        private ISqlSugarClient _db;
        private DataSeedExecuteHandler _dataSeedExecuteHandler;
        private ILogger<SystemDataJob> _logger;
        public SystemDataJob(ISqlSugarClient sqlSugarClient, DataSeedExecuteHandler dataSeedExecuteHandler,ILogger<SystemDataJob>  logger)
        {
            _db = sqlSugarClient;
            _dataSeedExecuteHandler=    dataSeedExecuteHandler;
            _logger= logger;
        }
        public async Task ExecuteAsync(JobExecutingContext context, CancellationToken stoppingToken)
        {
            _db.DbMaintenance.TruncateTable<UserEntity>();
            _db.DbMaintenance.TruncateTable<UserRoleEntity>();
            _db.DbMaintenance.TruncateTable<RoleEntity>();
            _db.DbMaintenance.TruncateTable<MenuEntity>();
            _db.DbMaintenance.TruncateTable<RoleMenuEntity>();
            _db.DbMaintenance.TruncateTable<DeptEntity>();
            _db.DbMaintenance.TruncateTable<PostEntity>();
            _db.DbMaintenance.TruncateTable<UserPostEntity>();
            _db.DbMaintenance.TruncateTable<ConfigEntity>();
            _db.DbMaintenance.TruncateTable<DictionaryEntity>();
            _db.DbMaintenance.TruncateTable<DictionaryTypeEntity>();
           await _dataSeedExecuteHandler.Invoker();
            _logger.LogWarning("数据已经重置还原");
        }
    }
}
