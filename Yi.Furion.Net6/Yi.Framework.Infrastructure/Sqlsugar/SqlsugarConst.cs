using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Infrastructure.Sqlsugar
{
    public class SqlsugarConst
    {
        public const string 读写分离为空 = "开启读写分离后，读库连接不能为空";

        public const string DbType配置为空 = "DbType配置为空，必须选择一个数据库类型";
    }
}
