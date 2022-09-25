using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Common.Enum;
using Yi.Framework.Model.Models;

namespace Yi.Framework.Model.SeedData
{
    public class MenuSeed : AbstractSeed<MenuEntity>
    {
        public override List<MenuEntity> GetSeed()
        {
            //系统管理
            MenuEntity system = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "系统管理",
                MenuType = MenuTypeEnum.Catalogue.GetHashCode(),
                Router = "/system",
                IsShow = true,
                IsLink = false,
                MenuIcon = "system",
                OrderNum = 100,
                ParentId = 0,
                IsDeleted = false
            };
            Entitys.Add(system);

            //系统监控
            MenuEntity monitoring = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "系统监控",
                MenuType = MenuTypeEnum.Catalogue.GetHashCode(),
                Router = "/monitor",
                IsShow = true,
                IsLink = false,
                MenuIcon = "monitor",
                OrderNum = 99,
                ParentId = 0,
                IsDeleted = false
            };
            Entitys.Add(monitoring);

            //系统工具
            MenuEntity tool = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "系统工具",
                MenuType = MenuTypeEnum.Catalogue.GetHashCode(),
                Router = "/tool",
                IsShow = true,
                IsLink = false,
                MenuIcon = "tool",
                OrderNum = 98,
                ParentId = 0,
                IsDeleted = false
            };
            Entitys.Add(tool);

            //Yi框架
            MenuEntity guide = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "Yi框架",
                MenuType = MenuTypeEnum.Catalogue.GetHashCode(),
                Router = "https://gitee.com/ccnetcore/yi",
                IsShow = true,
                IsLink = true,
                MenuIcon = "guide",
                OrderNum = 90,
                ParentId = 0,
                IsDeleted = false,
            };
            Entitys.Add(guide);


            //用户管理
            MenuEntity user = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "用户管理",
                PermissionCode = "system:user:list",
                MenuType = MenuTypeEnum.Menu.GetHashCode(),
                Router = "user",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "system/user/index",
                MenuIcon = "user",
                OrderNum = 100,
                ParentId = system.Id,
                IsDeleted = false
            };
            Entitys.Add(user);

            MenuEntity userQuery = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "用户查询",
                PermissionCode = "system:user:query",
                MenuType = MenuTypeEnum.Component.GetHashCode(),
                OrderNum = 100,
                ParentId = user.Id,
                IsDeleted = false
            };
            Entitys.Add(userQuery);

            MenuEntity userAdd = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "用户新增",
                PermissionCode = "system:user:add",
                MenuType = MenuTypeEnum.Component.GetHashCode(),
                OrderNum = 100,
                ParentId = user.Id,
                IsDeleted = false
            };
            Entitys.Add(userAdd);

            MenuEntity userEdit = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "用户修改",
                PermissionCode = "system:user:edit",
                MenuType = MenuTypeEnum.Component.GetHashCode(),
                OrderNum = 100,
                ParentId = user.Id,
                IsDeleted = false
            };
            Entitys.Add(userEdit);

            MenuEntity userRemove = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "用户删除",
                PermissionCode = "system:user:remove",
                MenuType = MenuTypeEnum.Component.GetHashCode(),
                OrderNum = 100,
                ParentId = user.Id,
                IsDeleted = false
            };
            Entitys.Add(userRemove);


            //角色管理
            MenuEntity role = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "角色管理",
                PermissionCode = "system:role:list",
                MenuType = MenuTypeEnum.Menu.GetHashCode(),
                Router = "role",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "system/role/index",
                MenuIcon = "peoples",
                OrderNum = 100,
                ParentId = system.Id,
                IsDeleted = false
            };
            Entitys.Add(role);

            MenuEntity roleQuery = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "角色查询",
                PermissionCode = "system:role:query",
                MenuType = MenuTypeEnum.Component.GetHashCode(),
                OrderNum = 100,
                ParentId = role.Id,
                IsDeleted = false
            };
            Entitys.Add(roleQuery);

            MenuEntity roleAdd = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "角色新增",
                PermissionCode = "system:role:add",
                MenuType = MenuTypeEnum.Component.GetHashCode(),
                OrderNum = 100,
                ParentId = role.Id,
                IsDeleted = false
            };
            Entitys.Add(roleAdd);

            MenuEntity roleEdit = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "角色修改",
                PermissionCode = "system:role:edit",
                MenuType = MenuTypeEnum.Component.GetHashCode(),
                OrderNum = 100,
                ParentId = role.Id,
                IsDeleted = false
            };
            Entitys.Add(roleEdit);

            MenuEntity roleRemove = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "角色删除",
                PermissionCode = "system:role:remove",
                MenuType = MenuTypeEnum.Component.GetHashCode(),
                OrderNum = 100,
                ParentId = role.Id,
                IsDeleted = false
            };
            Entitys.Add(roleRemove);


            //菜单管理
            MenuEntity menu = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "菜单管理",
                PermissionCode = "system:menu:list",
                MenuType = MenuTypeEnum.Menu.GetHashCode(),
                Router = "menu",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "system/menu/index",
                MenuIcon = "tree-table",
                OrderNum = 100,
                ParentId = system.Id,
                IsDeleted = false
            };
            Entitys.Add(menu);

            MenuEntity menuQuery = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "菜单查询",
                PermissionCode = "system:menu:query",
                MenuType = MenuTypeEnum.Component.GetHashCode(),
                OrderNum = 100,
                ParentId = menu.Id,
                IsDeleted = false
            };
            Entitys.Add(menuQuery);

            MenuEntity menuAdd = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "菜单新增",
                PermissionCode = "system:menu:add",
                MenuType = MenuTypeEnum.Component.GetHashCode(),
                OrderNum = 100,
                ParentId = menu.Id,
                IsDeleted = false
            };
            Entitys.Add(menuAdd);

            MenuEntity menuEdit = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "菜单修改",
                PermissionCode = "system:menu:edit",
                MenuType = MenuTypeEnum.Component.GetHashCode(),
                OrderNum = 100,
                ParentId = menu.Id,
                IsDeleted = false
            };
            Entitys.Add(menuEdit);

            MenuEntity menuRemove = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "菜单删除",
                PermissionCode = "system:menu:remove",
                MenuType = MenuTypeEnum.Component.GetHashCode(),
                OrderNum = 100,
                ParentId = menu.Id,
                IsDeleted = false
            };
            Entitys.Add(menuRemove);
  
            //部门管理
            MenuEntity dept = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "部门管理",
                PermissionCode = "system:dept:list",
                MenuType = MenuTypeEnum.Menu.GetHashCode(),
                Router = "dept",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "system/dept/index",
                MenuIcon = "tree",
                OrderNum = 100,
                ParentId = system.Id,
                IsDeleted = false
            };
            Entitys.Add(dept);

            MenuEntity deptQuery = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "部门查询",
                PermissionCode = "system:dept:query",
                MenuType = MenuTypeEnum.Component.GetHashCode(),
                OrderNum = 100,
                ParentId = dept.Id,
                IsDeleted = false
            };
            Entitys.Add(deptQuery);

            MenuEntity deptAdd = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "部门新增",
                PermissionCode = "system:dept:add",
                MenuType = MenuTypeEnum.Component.GetHashCode(),
                OrderNum = 100,
                ParentId = dept.Id,
                IsDeleted = false
            };
            Entitys.Add(deptAdd);

            MenuEntity deptEdit = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "部门修改",
                PermissionCode = "system:dept:edit",
                MenuType = MenuTypeEnum.Component.GetHashCode(),
                OrderNum = 100,
                ParentId = dept.Id,
                IsDeleted = false
            };
            Entitys.Add(deptEdit);

            MenuEntity deptRemove = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "部门删除",
                PermissionCode = "system:dept:remove",
                MenuType = MenuTypeEnum.Component.GetHashCode(),
                OrderNum = 100,
                ParentId = dept.Id,
                IsDeleted = false
            };
            Entitys.Add(deptRemove);



            //岗位管理
            MenuEntity post = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "岗位管理",
                PermissionCode = "system:post:list",
                MenuType = MenuTypeEnum.Menu.GetHashCode(),
                Router = "post",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "system/post/index",
                MenuIcon = "post",
                OrderNum = 100,
                ParentId = system.Id,
                IsDeleted = false
            };
            Entitys.Add(post);

            MenuEntity postQuery = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "岗位查询",
                PermissionCode = "system:post:query",
                MenuType = MenuTypeEnum.Component.GetHashCode(),
                OrderNum = 100,
                ParentId = post.Id,
                IsDeleted = false
            };
            Entitys.Add(postQuery);

            MenuEntity postAdd = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "岗位新增",
                PermissionCode = "system:post:add",
                MenuType = MenuTypeEnum.Component.GetHashCode(),
                OrderNum = 100,
                ParentId = post.Id,
                IsDeleted = false
            };
            Entitys.Add(postAdd);

            MenuEntity postEdit = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "岗位修改",
                PermissionCode = "system:post:edit",
                MenuType = MenuTypeEnum.Component.GetHashCode(),
                OrderNum = 100,
                ParentId = post.Id,
                IsDeleted = false
            };
            Entitys.Add(postEdit);

            MenuEntity postRemove = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "岗位删除",
                PermissionCode = "system:post:remove",
                MenuType = MenuTypeEnum.Component.GetHashCode(),
                OrderNum = 100,
                ParentId = post.Id,
                IsDeleted = false
            };
            Entitys.Add(postRemove);

            //字典管理
            MenuEntity dict = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "字典管理",
                PermissionCode = "system:dict:list",
                MenuType = MenuTypeEnum.Menu.GetHashCode(),
                Router = "dict",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "system/dict/index",
                MenuIcon = "dict",
                OrderNum = 100,
                ParentId = system.Id,
                IsDeleted = false
            };
            Entitys.Add(dict);

            MenuEntity dictQuery = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "字典查询",
                PermissionCode = "system:dict:query",
                MenuType = MenuTypeEnum.Component.GetHashCode(),
                OrderNum = 100,
                ParentId = dict.Id,
                IsDeleted = false
            };
            Entitys.Add(dictQuery);

            MenuEntity dictAdd = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "字典新增",
                PermissionCode = "system:dict:add",
                MenuType = MenuTypeEnum.Component.GetHashCode(),
                OrderNum = 100,
                ParentId = dict.Id,
                IsDeleted = false
            };
            Entitys.Add(dictAdd);

            MenuEntity dictEdit = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "字典修改",
                PermissionCode = "system:dict:edit",
                MenuType = MenuTypeEnum.Component.GetHashCode(),
                OrderNum = 100,
                ParentId = dict.Id,
                IsDeleted = false
            };
            Entitys.Add(dictEdit);

            MenuEntity dictRemove = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "字典删除",
                PermissionCode = "system:dict:remove",
                MenuType = MenuTypeEnum.Component.GetHashCode(),
                OrderNum = 100,
                ParentId = dict.Id,
                IsDeleted = false
            };
            Entitys.Add(dictRemove);


            //参数设置
            MenuEntity config = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "参数设置",
                PermissionCode = "system:config:list",
                MenuType = MenuTypeEnum.Menu.GetHashCode(),
                Router = "config",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "system/config/index",
                MenuIcon = "edit",
                OrderNum = 100,
                ParentId = system.Id,
                IsDeleted = false
            };
            Entitys.Add(config);

            MenuEntity configQuery = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "参数查询",
                PermissionCode = "system:config:query",
                MenuType = MenuTypeEnum.Component.GetHashCode(),
                OrderNum = 100,
                ParentId = config.Id,
                IsDeleted = false
            };
            Entitys.Add(configQuery);

            MenuEntity configAdd = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "参数新增",
                PermissionCode = "system:config:add",
                MenuType = MenuTypeEnum.Component.GetHashCode(),
                OrderNum = 100,
                ParentId = config.Id,
                IsDeleted = false
            };
            Entitys.Add(configAdd);

            MenuEntity configEdit = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "参数修改",
                PermissionCode = "system:config:edit",
                MenuType = MenuTypeEnum.Component.GetHashCode(),
                OrderNum = 100,
                ParentId = config.Id,
                IsDeleted = false
            };
            Entitys.Add(configEdit);

            MenuEntity configRemove = new MenuEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                MenuName = "参数删除",
                PermissionCode = "system:config:remove",
                MenuType = MenuTypeEnum.Component.GetHashCode(),
                OrderNum = 100,
                ParentId = config.Id,
                IsDeleted = false
            };
            Entitys.Add(configRemove);


            return Entitys;
        }
    }
}
