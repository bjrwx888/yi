using SqlSugar;
using Yi.Framework.Common.Models;
using Yi.Framework.Model.Models;

namespace Yi.Framework.Repository
{
    public class DataContext<T> : SimpleClient<T> where T : class, IBaseModelEntity, new()
    {
        public DataContext(ISqlSugarClient context) : base(context)
        {
            Db =base.Context;
        }
        
        public ISqlSugarClient Db;
    }
}
