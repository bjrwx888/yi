﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using Yi.Framework.Infrastructure.Data.Auditing;
using Yi.Framework.Infrastructure.Ddd.Entities;

namespace Yi.Framework.Module.WebFirstManager.Entities
{
    public class TemplateEntity : IEntity<long>, ICreationAuditedObject
    {

        [SugarColumn(ColumnName = "Id", IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 模板字符串
        /// </summary>
        public string TemplateStr { get; set; } = string.Empty;


        public long? CreatorId { get; set; }

        public DateTime CreationTime { get; set; }
    }
}