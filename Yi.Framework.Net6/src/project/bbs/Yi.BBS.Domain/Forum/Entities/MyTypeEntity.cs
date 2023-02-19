﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Data.Entities;
using Yi.Framework.Ddd.Entities;

namespace Yi.BBS.Domain.Forum.Entities
{
    [SugarTable("MyType")]
    public class MyTypeEntity : IEntity<long>, ISoftDelete
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }
        public bool IsDeleted { get; set; }

        public string Name { get; set; }
        public string? Color { get; set; }
        public string? BackgroundColor { get; set; }

        public long UserId { get; set; }
    }
}