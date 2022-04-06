using SqlSugar;
using Yi.Framework.Common.Models;

namespace Yi.Framework.Repository
{
    public class DataContext<T> : SimpleClient<T> where T : class, new()
    {
        public DataContext(ISqlSugarClient context) : base(context)
        {
            Db =base.Context;
        }
        
        public ISqlSugarClient Db;
    }
}
