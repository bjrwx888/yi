using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Data.Auditing;
using Yi.Framework.Ddd.Entities;

namespace Yi.Framework.FileManager
{
    /// <summary>
    /// 文件表
    ///</summary>
    [SugarTable("File")]
    public class FileEntity : IEntity<long>,IAuditedObject
    {
        [SugarColumn(ColumnName = "Id", IsPrimaryKey = true)]
        public long Id { get; set; }
        /// <summary>
        /// 文件类型 
        ///</summary>
        [SugarColumn(ColumnName = "FileContentType")]
        public string? FileContentType { get; set; }
        /// <summary>
        /// 文件大小 
        ///</summary>
        [SugarColumn(ColumnName = "FileSize")]
        public decimal FileSize { get; set; }
        /// <summary>
        /// 文件名 
        ///</summary>
        [SugarColumn(ColumnName = "FileName")]
        public string FileName { get; set; }
        /// <summary>
        /// 文件路径 
        ///</summary>
        [SugarColumn(ColumnName = "FilePath")]
        public string FilePath { get; set; }

        public DateTime CreationTime { get; set; }
        public long? CreatorId { get; set; }

        public long? LastModifierId { get; set; }

        public DateTime? LastModificationTime { get; set; }
    }
}
