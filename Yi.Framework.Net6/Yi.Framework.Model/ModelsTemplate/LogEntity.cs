﻿using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
namespace Yi.Framework.Model.Models
{
    /// <summary>
    /// 日志表
    ///</summary>
    public partial class LogEntity:IBaseModelEntity
    {
        public LogEntity()
        {
            this.IsDeleted = false;
            this.CreateTime = DateTime.Now;
        }
        /// <summary>
        /// 1 
        ///</summary>
         [SugarColumn(ColumnName="Id" ,IsPrimaryKey = true   )]
         public long Id { get; set; }

        /// <summary>
        /// 创建者 
        ///</summary>
         [SugarColumn(ColumnName="CreateUser"    )]
         public long? CreateUser { get; set; }
        /// <summary>
        /// 创建时间 
        ///</summary>
         [SugarColumn(ColumnName="CreateTime"    )]
         public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 修改者 
        ///</summary>
         [SugarColumn(ColumnName="ModifyUser"    )]
         public long? ModifyUser { get; set; }
        /// <summary>
        /// 修改时间 
        ///</summary>
         [SugarColumn(ColumnName="ModifyTime"    )]
         public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// 是否删除 
        ///</summary>
         [SugarColumn(ColumnName="IsDeleted"    )]
         public bool? IsDeleted { get; set; }
        /// <summary>
        /// 租户Id 
        ///</summary>
         [SugarColumn(ColumnName="TenantId"    )]
         public long? TenantId { get; set; }
        /// <summary>
        /// 消息 
        ///</summary>
         [SugarColumn(ColumnName="Message"    )]
         public string Message { get; set; }
    }
}