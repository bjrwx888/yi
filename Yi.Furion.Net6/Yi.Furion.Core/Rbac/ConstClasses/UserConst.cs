using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Furion.Core.Rbac.ConstClasses
{
    /// <summary>
    /// 常量定义
    /// </summary>

    public class UserConst
    {
        public const string 登录失败_错误 = "登录失败！用户名或密码错误！";
        public const string 登录失败_不存在 = "登录失败！用户名不存在！";
        public const string 添加失败_密码为空 = "密码为空，添加失败！";
        public const string 添加失败_用户存在 = "用户已经存在，添加失败！";
        public const string 用户无权限分配 = "登录禁用！该用户分配无任何权限，无意义登录！";
        public const string 用户无角色分配 = "登录禁用！该用户分配无任何角色，无意义登录！";

        public const string Admin = "cc";
        public const string AdminRolesCode = "admin";
        public const string AdminPermissionCode = "*:*:*";

        public const string GuestRoleCode = "guest";
        public const string CommonRoleName = "common";
    }
}
