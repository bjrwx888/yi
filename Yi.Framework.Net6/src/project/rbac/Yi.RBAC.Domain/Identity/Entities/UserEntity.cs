using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Data.Auditing;
using Yi.Framework.Data.Entities;
using Yi.Framework.Ddd.Entities;

namespace Yi.RBAC.Domain.Identity.Entities
{
    /// <summary>
    /// 用户表
    /// </summary>
    [SugarTable("User")]
    public class UserEntity : IEntity<long>, ISoftDelete, IAuditedObject, IOrderNum
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int? Age { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// 加密盐值
        /// </summary>
        public string Salt { get; set; } = string.Empty;

        /// <summary>
        /// 头像
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string? Nick { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Ip
        /// </summary>
        public string? Ip { get; set; }

        /// <summary>
        /// 地址
        /// </summary>

        public string? Address { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public long? Phone { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string? Introduction { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public SexEnum Sex { get; set; } = SexEnum.Unknown;

        /// <summary>
        /// 部门id
        /// </summary>
        public long? DeptId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 创建者
        /// </summary>
        public long? CreatorId { get; set; }

        /// <summary>
        /// 最后修改者
        /// </summary>
        public long? LastModifierId { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; } = 0;
    }

    /// <summary>
    /// 性别
    /// </summary>
    public enum SexEnum
    {
        /// <summary>
        /// 男性
        /// </summary>
        Male = 0,
        /// <summary>
        /// 女性
        /// </summary>
        Woman = 1,
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = 2

    }
}
