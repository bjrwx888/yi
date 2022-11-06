//using SqlSugar;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Yi.Framework.Model
//{
//    //需要在用户表中关联好该租户信息，一个用户关联一个租户
//    //不同租户下，用户可以相同
//    //用户登录后，token中可包含租户id，同时缓存一份用户信息(包含租户信息)
//    [Tenant("0")]
//    //当然，像用户、角色、菜单、租户为共享库了
//    [SugarTable("Tenant")]
//    public class TenantEntity
//    {
//        /// <summary>
//        /// 主键唯一标识
//        /// </summary>
//        [SugarColumn(IsPrimaryKey = true)]
//        public long Id { get; set; }

//        /// <summary>
//        /// 租户id
//        /// </summary>
//        public string? TenantId { get; set;  }

//        /// <summary>
//        /// 业务库连接字符串
//        /// </summary>
//        public string? Connection { get; set; }

//        /// <summary>
//        /// 业务库连接类型
//        /// </summary>
//        public string? DbType { get; set; }


//    }
//}
