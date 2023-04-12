﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Infrastructure.Sqlsugar
{
    public class DbConnOptions
    {
        /// <summary>
        /// 连接字符串，必填
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public DbType? DbType { get; set; }

        /// <summary>
        /// 开启种子数据
        /// </summary>
        public bool EnabledDbSeed { get; set; } = false;

        /// <summary>
        /// 开启读写分离
        /// </summary>
        public bool EnabledReadWrite { get; set; } = false;

        /// <summary>
        /// 开启codefirst
        /// </summary>
        public bool EnabledCodeFirst { get; set; } = false;

        /// <summary>
        /// 实体程序集
        /// </summary>
        public List<string>? EntityAssembly { get; set; }

        /// <summary>
        /// 读写分离
        /// </summary>
        public List<string>? ReadUrl { get; set; }
    }
}
