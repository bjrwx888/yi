using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
namespace Yi.Framework.Model.Models
{
    /// <summary>
    /// 日志表
    ///</summary>
    [SplitTable(SplitType.Year)]
    [SugarTable("SplitLog_{year}{month}{day}")]
    public partial class LogEntity
    {
        [SplitField] //分表字段 在插入的时候会根据这个字段插入哪个表，在更新删除的时候用这个字段找出相关表
        public DateTime? LogCreateTime { get; set; }
    }
}
