using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Auditing;
using Volo.Abp.DependencyInjection;

namespace Yi.Framework.SqlSugarCore
{
    public class SqlSugarLogAuditingStore : IAuditingStore, ISingletonDependency
    {
        public Task SaveAsync(AuditLogInfo auditInfo)
        {
            Console.WriteLine(auditInfo.ExecutionTime);
            return Task.CompletedTask;
        }
    }
}
