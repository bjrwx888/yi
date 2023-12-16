using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Rbac.Domain.Shared.Options
{
    public class RbacOptions
    {
        /// <summary>
        /// 超级管理员默认密码
        /// </summary>
        public string AdminPassword { get; set; } = "123456";
    }
}
