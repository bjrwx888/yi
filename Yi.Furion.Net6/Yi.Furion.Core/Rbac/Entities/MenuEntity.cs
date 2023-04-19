﻿using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
using Yi.Framework.Infrastructure.Data.Auditing;
using Yi.Framework.Infrastructure.Data.Entities;
using Yi.Framework.Infrastructure.Ddd.Entities;
using Yi.Framework.Infrastructure.Helper;
using Yi.Furion.Core.Rbac.Dtos;
using Yi.Furion.Core.Rbac.Enums;

namespace Yi.Furion.Core.Rbac.Entities
{
    /// <summary>
    /// 菜单表
    ///</summary>
    [SugarTable("Menu")]
    public partial class MenuEntity : IEntity<long>, ISoftDelete, IAuditedObject, IOrderNum, IState
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 创建者
        /// </summary>
        public long? CreatorId { get; set; }

        /// <summary>
        /// 最后修改者
        /// </summary>
        public long? LastModifierId { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 状态
        /// </summary>
        public bool State { get; set; }

        /// <summary>
        /// 菜单名
        /// </summary>
        public string MenuName { get; set; } = string.Empty;
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "MenuType")]
        public MenuTypeEnum MenuType { get; set; } = MenuTypeEnum.Menu;
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PermissionCode")]
        public string PermissionCode { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "ParentId")]
        public long ParentId { get; set; }

        /// <summary>
        /// 菜单图标 
        ///</summary>
        [SugarColumn(ColumnName = "MenuIcon")]
        public string MenuIcon { get; set; }
        /// <summary>
        /// 菜单组件路由 
        ///</summary>
        [SugarColumn(ColumnName = "Router")]
        public string Router { get; set; }
        /// <summary>
        /// 是否为外部链接 
        ///</summary>
        [SugarColumn(ColumnName = "IsLink")]
        public bool IsLink { get; set; }
        /// <summary>
        /// 是否缓存 
        ///</summary>
        [SugarColumn(ColumnName = "IsCache")]
        public bool IsCache { get; set; }
        /// <summary>
        /// 是否显示 
        ///</summary>
        [SugarColumn(ColumnName = "IsShow")]
        public bool IsShow { get; set; } = true;

        /// <summary>
        /// 描述 
        ///</summary>
        [SugarColumn(ColumnName = "Remark")]
        public string Remark { get; set; }
        /// <summary>
        /// 组件路径 
        ///</summary>
        [SugarColumn(ColumnName = "Component")]
        public string Component { get; set; }
        /// <summary>
        /// 路由参数 
        ///</summary>
        [SugarColumn(ColumnName = "Query")]
        public string Query { get; set; }

        [SugarColumn(IsIgnore = true)]
        public List<MenuEntity> Children { get; set; }
    }

    /// <summary>
    /// 实体扩展
    /// </summary>
    public static class MenuEntityExtensions
    {
        /// <summary>
        /// 构建vue3路由
        /// </summary>
        /// <param name="menus"></param>
        /// <returns></returns>
        public static List<Vue3RouterDto> Vue3RouterBuild(this List<MenuEntity> menus)
        {
            menus = menus.Where(m => m.MenuType != MenuTypeEnum.Component).ToList();
            List<Vue3RouterDto> routers = new();
            foreach (var m in menus)
            {

                var r = new Vue3RouterDto();
                r.OrderNum = m.OrderNum;
                var routerName = m.Router?.Split("/").LastOrDefault();
                r.Id = m.Id;
                r.ParentId = m.ParentId;

                //开头大写
                r.Name = routerName?.First().ToString().ToUpper() + routerName?.Substring(1);
                r.Path = m.Router!;
                r.Hidden = !m.IsShow;


                if (m.MenuType == MenuTypeEnum.Catalogue)
                {
                    r.Redirect = "noRedirect";
                    r.AlwaysShow = true;

                    //判断是否为最顶层的路由
                    if (0 == m.ParentId)
                    {
                        r.Component = "Layout";
                    }
                    else
                    {
                        r.Component = "ParentView";
                    }
                }
                if (m.MenuType == MenuTypeEnum.Menu)
                {
                    r.Redirect = "noRedirect";
                    r.AlwaysShow = true;
                    r.Component = m.Component!;
                    r.AlwaysShow = false;
                }
                r.Meta = new Meta
                {
                    Title = m.MenuName!,
                    Icon = m.MenuIcon!,
                    NoCache = !m.IsCache
                };
                if (m.IsLink)
                {
                    r.Meta.link = m.Router!;
                    r.AlwaysShow = false;
                }

                routers.Add(r);
            }
            return TreeHelper.SetTree(routers);

        }
    }
}