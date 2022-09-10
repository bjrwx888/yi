using SqlSugar;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class LogService 
    {
        public async Task<List<long>> AddListTest(List<LogEntity> logEntities)
        {
            return await _repository._Db.Insertable(logEntities).SplitTable().ExecuteReturnSnowflakeIdListAsync();
        }

        public async Task<List<LogEntity>> GetListTest()
        {
            return await _repository._DbQueryable.SplitTable(tas => tas.Where(u => u.TableName.Contains("2020") || u.TableName.Contains("2021"))).ToListAsync();
        }
    }
}
