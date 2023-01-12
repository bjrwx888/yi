using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Core.Enum
{
    public enum ResultCodeEnum
    {
        /// <summary>
        /// 操作成功。
        /// </summary>
        Success = 200,

        /// <summary>
        /// 操作不成功
        /// </summary>
        NotSuccess = 500,

        /// <summary>
        /// 无权限
        /// </summary>
        NoPermission = 401
    }
}
