using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
using Yi.Framework.Common.Enum;
using Yi.Framework.Common.Models;

namespace Yi.Framework.Model.Models
{
    /// <summary>
    /// 菜单表
    ///</summary>
    public partial class MenuEntity
    {
        [SqlSugar.SugarColumn(IsIgnore = true)]
        public List<MenuEntity> Children { get; set; }


        public static List<VueRouterModel> RouterBuild(List<MenuEntity> menus)
        {
            menus = menus.Where(m => m.MenuType != null && m.MenuType != MenuTypeEnum.Component.GetHashCode()).ToList();
            List<VueRouterModel> routers = new();
            foreach (var m in menus)
            {

                var r = new VueRouterModel();
                r.OrderNum = m.OrderNum ?? 0;
                var routerName = m.Router?.Split("/").LastOrDefault();
                r.Id = m.Id;
                r.ParentId = m.ParentId??-1;

                //开头大写
                r.Name = routerName?.First().ToString().ToUpper() + routerName?.Substring(1);
                r.Path = m.Router;
                r.Hidden =!m.IsShow??false;


                if (m.MenuType == MenuTypeEnum.Catalogue.GetHashCode())
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
                if (m.MenuType == MenuTypeEnum.Menu.GetHashCode())
                {

                    r.Redirect = "noRedirect";
                    r.AlwaysShow = true;
                    r.Component = m.Component;
                    r.AlwaysShow = false;
                }
                r.Meta = new Meta
                {
                    Title = m.MenuName,
                    Icon = m.MenuIcon,
                    NoCache = !m.IsCache??true
                };
                if (m.IsLink??false)
                {
                    r.Meta.link = m.Router;
                    r.AlwaysShow = false;
                }

                routers.Add(r);
            }
            return Common.Helper.TreeHelper.SetTree(routers);
        }

    }
}
