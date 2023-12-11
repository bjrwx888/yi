﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using Volo.Abp.DependencyInjection;

namespace Yi.Framework.SqlSugarCore.Abstractions
{
    public interface ISqlSugarDbContext
    {
        //  IAbpLazyServiceProvider LazyServiceProvider { get; set; }
        ISqlSugarClient SqlSugarClient { get; }
    }
}
