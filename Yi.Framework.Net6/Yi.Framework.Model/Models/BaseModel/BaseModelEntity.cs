using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Model.Models
{
    public abstract class BaseModelEntity
    {
        public BaseModelEntity()
        {
            this.Id = Guid.NewGuid();
            this.IsDeleted = false;
            this.CreateTime = DateTime.Now;
        }
        /// <summary>
        /// 1 
        ///</summary>
        [SugarColumn(ColumnName = "Id", IsPrimaryKey = true)]
        public Guid Id { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        [SugarColumn(ColumnName = "CreateUser")]
        public Guid? CreateUser { get; set; }
        /// <summary>
        /// 修改者 
        ///</summary>
        [SugarColumn(ColumnName = "ModifyUser")]
        public Guid? ModifyUser { get; set; }
        /// <summary>
        /// 创建时间 
        ///</summary>
        [SugarColumn(ColumnName = "CreateTime")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 修改时间 
        ///</summary>
        [SugarColumn(ColumnName = "ModifyTime")]
        public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// 删除者 
        ///</summary>
        [SugarColumn(ColumnName = "DeleteUser")]
        public Guid? DeleteUser { get; set; }
        /// <summary>
        /// 删除时间 
        ///</summary>
        [SugarColumn(ColumnName = "DeleteTime")]
        public DateTime? DeleteTime { get; set; }
        /// <summary>
        /// 是否删除 
        ///</summary>
        [SugarColumn(ColumnName = "IsDeleted")]
        public bool? IsDeleted { get; set; }
    }
}
