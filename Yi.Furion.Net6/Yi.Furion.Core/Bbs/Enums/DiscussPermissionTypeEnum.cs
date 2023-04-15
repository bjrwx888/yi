using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Furion.Core.Bbs.Enums
{
    public enum DiscussPermissionTypeEnum
    {
        /// <summary>
        /// 默认：公开
        /// </summary>
        Public = 0,

        /// <summary>
        /// 仅自己可见
        /// </summary>
        Oneself,

        /// <summary>
        /// 部分用户可见
        /// </summary>
        User

    }
}
