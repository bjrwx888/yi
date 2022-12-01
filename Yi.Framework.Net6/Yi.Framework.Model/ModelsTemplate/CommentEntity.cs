using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using SqlSugar;
namespace Yi.Framework.Model.Models
{
    /// <summary>
    /// 评论表
    ///</summary>
    [SugarTable("Comment")]
    public partial class CommentEntity:IBaseModelEntity
    {
        public CommentEntity()
        {
            this.CreateTime = DateTime.Now;
            this.AgreeNum= 0;
            this.CommentNum = 0;
            this.ParentId= 0;
        }
        [JsonConverter(typeof(ValueToStringConverter))]
        [SugarColumn(ColumnName="Id" ,IsPrimaryKey = true   )]
         public long Id { get; set; }
        /// <summary>
        /// 文章id 
        ///</summary>
         [SugarColumn(ColumnName="ArticleId"    )]
         public long? ArticleId { get; set; }
        /// <summary>
        /// 被回复用户id 
        ///</summary>
         [SugarColumn(ColumnName="UserId"    )]
         public long? UserId { get; set; }
        /// <summary>
        /// 评论内容 
        ///</summary>
         [SugarColumn(ColumnName="Content"    )]
         public string? Content { get; set; }
        /// <summary>
        /// 点赞数 
        ///</summary>
         [SugarColumn(ColumnName="AgreeNum"    )]
         public int? AgreeNum { get; set; }
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
        /// 排序字段 
        ///</summary>
         [SugarColumn(ColumnName="OrderNum"    )]
         public int? OrderNum { get; set; }
        /// <summary>
        /// 描述 
        ///</summary>
         [SugarColumn(ColumnName="Remark"    )]
         public string? Remark { get; set; }
        /// <summary>
        /// 子评论数 
        ///</summary>
         [SugarColumn(ColumnName="CommentNum"    )]
         public int? CommentNum { get; set; }
        /// <summary>
        /// 父级评论id 
        ///</summary>
         [SugarColumn(ColumnName="ParentId"    )]
         public long? ParentId { get; set; }
    }
}
