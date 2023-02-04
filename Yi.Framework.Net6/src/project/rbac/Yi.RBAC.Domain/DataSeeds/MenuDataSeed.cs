using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Data.DataSeeds;
using Yi.Framework.Ddd.Repositories;
using Yi.RBAC.Domain.Identity.Entities;
using Yi.RBAC.Domain.Shared.Identity.EnumClasses;

namespace Yi.RBAC.Domain.DataSeeds
{
    [AppService(typeof(IDataSeed))]
    public class MenuDataSeed : AbstractDataSeed<MenuEntity>
    {
        public MenuDataSeed(IRepository<MenuEntity> repository) : base(repository)
        {
        }

        public override List<MenuEntity> GetSeedData()
        {
            List<MenuEntity> entities = new List<MenuEntity>();
            //系统管理
            MenuEntity system = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "系统管理",
                MenuType = MenuTypeEnum.Catalogue,
                Router = "/system",
                IsShow = true,
                IsLink = false,
                MenuIcon = "system",
                OrderNum = 100,
                ParentId = 0,
                IsDeleted = false
            };
            entities.Add(system);

            //系统监控
            MenuEntity monitoring = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "系统监控",
                MenuType = MenuTypeEnum.Catalogue,
                Router = "/monitor",
                IsShow = true,
                IsLink = false,
                MenuIcon = "monitor",
                OrderNum = 99,
                ParentId = 0,
                IsDeleted = false
            };
            entities.Add(monitoring);


            //在线用户
            MenuEntity online = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "在线用户",
                PermissionCode = "monitor:online:list",
                MenuType = MenuTypeEnum.Menu,
                Router = "online",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "monitor/online/index",
                MenuIcon = "online",
                OrderNum = 100,
                ParentId = monitoring.Id,
                IsDeleted = false
            };
            entities.Add(online);




            //系统工具
            MenuEntity tool = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "系统工具",
                MenuType = MenuTypeEnum.Catalogue,
                Router = "/tool",
                IsShow = true,
                IsLink = false,
                MenuIcon = "tool",
                OrderNum = 98,
                ParentId = 0,
                IsDeleted = false
            };
            entities.Add(tool);
            //swagger文档
            MenuEntity swagger = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "接口文档",
                MenuType = MenuTypeEnum.Menu,
                Router = "http://localhost:19001",
                IsShow = true,
                IsLink = true,
                MenuIcon = "list",
                OrderNum = 100,
                ParentId = tool.Id,
                IsDeleted = false,
            };
            entities.Add(swagger);


            //BBS
            MenuEntity bbs = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "BBS",
                MenuType = MenuTypeEnum.Catalogue,
                Router = "/bbs",
                IsShow = true,
                IsLink = false,
                MenuIcon = "international",
                OrderNum = 97,
                ParentId = 0,
                IsDeleted = false
            };
            entities.Add(bbs);
            //文章管理
            MenuEntity article = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "文章管理",
                PermissionCode = "bbs:article:list",
                MenuType = MenuTypeEnum.Menu,
                Router = "article",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "bbs/article/index",
                MenuIcon = "education",
                OrderNum = 100,
                ParentId = bbs.Id,
                IsDeleted = false
            };
            entities.Add(article);

            MenuEntity articleQuery = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "文章查询",
                PermissionCode = "bbs:article:query",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = article.Id,
                IsDeleted = false
            };
            entities.Add(articleQuery);

            MenuEntity articleAdd = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "文章新增",
                PermissionCode = "bbs:article:add",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = article.Id,
                IsDeleted = false
            };
            entities.Add(articleAdd);

            MenuEntity articleEdit = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "文章修改",
                PermissionCode = "bbs:article:edit",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = article.Id,
                IsDeleted = false
            };
            entities.Add(articleEdit);

            MenuEntity articleRemove = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "文章删除",
                PermissionCode = "bbs:article:remove",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = article.Id,
                IsDeleted = false
            };
            entities.Add(articleRemove);

            //ERP
            MenuEntity erp = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "ERP",
                MenuType = MenuTypeEnum.Catalogue,
                Router = "/erp",
                IsShow = true,
                IsLink = false,
                MenuIcon = "international",
                OrderNum = 96,
                ParentId = 0,
                IsDeleted = false
            };
            entities.Add(erp);



            //供应商定义
            MenuEntity supplier = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "供应商定义",
                PermissionCode = "erp:supplier:list",
                MenuType = MenuTypeEnum.Menu,
                Router = "supplier",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "erp/supplier/index",
                MenuIcon = "education",
                OrderNum = 100,
                ParentId = erp.Id,
                IsDeleted = false
            };
            entities.Add(supplier);

            MenuEntity supplierQuery = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "供应商查询",
                PermissionCode = "erp:supplier:query",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = supplier.Id,
                IsDeleted = false
            };
            entities.Add(supplierQuery);

            MenuEntity supplierAdd = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "供应商新增",
                PermissionCode = "erp:supplier:add",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = supplier.Id,
                IsDeleted = false
            };
            entities.Add(supplierAdd);

            MenuEntity supplierEdit = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "供应商修改",
                PermissionCode = "erp:supplier:edit",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = supplier.Id,
                IsDeleted = false
            };
            entities.Add(supplierEdit);

            MenuEntity supplierRemove = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "供应商删除",
                PermissionCode = "erp:supplier:remove",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = supplier.Id,
                IsDeleted = false
            };
            entities.Add(supplierRemove);


            //仓库定义
            MenuEntity warehouse = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "仓库定义",
                PermissionCode = "erp:warehouse:list",
                MenuType = MenuTypeEnum.Menu,
                Router = "warehouse",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "erp/warehouse/index",
                MenuIcon = "education",
                OrderNum = 100,
                ParentId = erp.Id,
                IsDeleted = false
            };
            entities.Add(warehouse);

            MenuEntity warehouseQuery = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "仓库查询",
                PermissionCode = "erp:warehouse:query",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = warehouse.Id,
                IsDeleted = false
            };
            entities.Add(warehouseQuery);

            MenuEntity warehouseAdd = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "仓库新增",
                PermissionCode = "erp:warehouse:add",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = warehouse.Id,
                IsDeleted = false
            };
            entities.Add(warehouseAdd);

            MenuEntity warehouseEdit = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "仓库修改",
                PermissionCode = "erp:warehouse:edit",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = warehouse.Id,
                IsDeleted = false
            };
            entities.Add(warehouseEdit);

            MenuEntity warehouseRemove = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "仓库删除",
                PermissionCode = "erp:warehouse:remove",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = warehouse.Id,
                IsDeleted = false
            };
            entities.Add(warehouseRemove);


            //单位定义
            MenuEntity unit = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "单位定义",
                PermissionCode = "erp:unit:list",
                MenuType = MenuTypeEnum.Menu,
                Router = "unit",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "erp/unit/index",
                MenuIcon = "education",
                OrderNum = 100,
                ParentId = erp.Id,
                IsDeleted = false
            };
            entities.Add(unit);

            MenuEntity unitQuery = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "单位查询",
                PermissionCode = "erp:unit:query",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = unit.Id,
                IsDeleted = false
            };
            entities.Add(unitQuery);

            MenuEntity unitAdd = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "单位新增",
                PermissionCode = "erp:unit:add",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = unit.Id,
                IsDeleted = false
            };
            entities.Add(unitAdd);

            MenuEntity unitEdit = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "单位修改",
                PermissionCode = "erp:unit:edit",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = unit.Id,
                IsDeleted = false
            };
            entities.Add(unitEdit);

            MenuEntity unitRemove = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "单位删除",
                PermissionCode = "erp:unit:remove",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = unit.Id,
                IsDeleted = false
            };
            entities.Add(unitRemove);


            //物料定义
            MenuEntity material = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "物料定义",
                PermissionCode = "erp:material:list",
                MenuType = MenuTypeEnum.Menu,
                Router = "material",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "erp/material/index",
                MenuIcon = "education",
                OrderNum = 100,
                ParentId = erp.Id,
                IsDeleted = false
            };
            entities.Add(material);

            MenuEntity materialQuery = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "物料查询",
                PermissionCode = "erp:material:query",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = material.Id,
                IsDeleted = false
            };
            entities.Add(materialQuery);

            MenuEntity materialAdd = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "物料新增",
                PermissionCode = "erp:material:add",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = material.Id,
                IsDeleted = false
            };
            entities.Add(materialAdd);

            MenuEntity materialEdit = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "物料修改",
                PermissionCode = "erp:material:edit",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = material.Id,
                IsDeleted = false
            };
            entities.Add(materialEdit);

            MenuEntity materialRemove = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "物料删除",
                PermissionCode = "erp:material:remove",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = material.Id,
                IsDeleted = false
            };
            entities.Add(materialRemove);


            //采购订单
            MenuEntity purchase = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "采购订单",
                PermissionCode = "erp:purchase:list",
                MenuType = MenuTypeEnum.Menu,
                Router = "purchase",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "erp/purchase/index",
                MenuIcon = "education",
                OrderNum = 100,
                ParentId = erp.Id,
                IsDeleted = false
            };
            entities.Add(purchase);

            MenuEntity purchaseQuery = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "采购订单查询",
                PermissionCode = "erp:purchase:query",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = purchase.Id,
                IsDeleted = false
            };
            entities.Add(purchaseQuery);

            MenuEntity purchaseAdd = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "采购订单新增",
                PermissionCode = "erp:purchase:add",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = purchase.Id,
                IsDeleted = false
            };
            entities.Add(purchaseAdd);

            MenuEntity purchaseEdit = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "采购订单修改",
                PermissionCode = "erp:purchase:edit",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = purchase.Id,
                IsDeleted = false
            };
            entities.Add(purchaseEdit);

            MenuEntity purchaseRemove = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "采购订单删除",
                PermissionCode = "erp:purchase:remove",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = purchase.Id,
                IsDeleted = false
            };
            entities.Add(purchaseRemove);



            //Yi框架
            MenuEntity guide = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "Yi框架",
                MenuType = MenuTypeEnum.Catalogue,
                Router = "https://gitee.com/ccnetcore/yi",
                IsShow = true,
                IsLink = true,
                MenuIcon = "guide",
                OrderNum = 90,
                ParentId = 0,
                IsDeleted = false,
            };
            entities.Add(guide);

            //用户管理
            MenuEntity user = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "用户管理",
                PermissionCode = "system:user:list",
                MenuType = MenuTypeEnum.Menu,
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
            entities.Add(user);

            MenuEntity userQuery = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "用户查询",
                PermissionCode = "system:user:query",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = user.Id,
                IsDeleted = false
            };
            entities.Add(userQuery);

            MenuEntity userAdd = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "用户新增",
                PermissionCode = "system:user:add",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = user.Id,
                IsDeleted = false
            };
            entities.Add(userAdd);

            MenuEntity userEdit = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "用户修改",
                PermissionCode = "system:user:edit",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = user.Id,
                IsDeleted = false
            };
            entities.Add(userEdit);

            MenuEntity userRemove = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "用户删除",
                PermissionCode = "system:user:remove",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = user.Id,
                IsDeleted = false
            };
            entities.Add(userRemove);


            //角色管理
            MenuEntity role = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "角色管理",
                PermissionCode = "system:role:list",
                MenuType = MenuTypeEnum.Menu,
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
            entities.Add(role);

            MenuEntity roleQuery = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "角色查询",
                PermissionCode = "system:role:query",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = role.Id,
                IsDeleted = false
            };
            entities.Add(roleQuery);

            MenuEntity roleAdd = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "角色新增",
                PermissionCode = "system:role:add",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = role.Id,
                IsDeleted = false
            };
            entities.Add(roleAdd);

            MenuEntity roleEdit = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "角色修改",
                PermissionCode = "system:role:edit",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = role.Id,
                IsDeleted = false
            };
            entities.Add(roleEdit);

            MenuEntity roleRemove = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "角色删除",
                PermissionCode = "system:role:remove",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = role.Id,
                IsDeleted = false
            };
            entities.Add(roleRemove);


            //菜单管理
            MenuEntity menu = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "菜单管理",
                PermissionCode = "system:menu:list",
                MenuType = MenuTypeEnum.Menu,
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
            entities.Add(menu);

            MenuEntity menuQuery = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "菜单查询",
                PermissionCode = "system:menu:query",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = menu.Id,
                IsDeleted = false
            };
            entities.Add(menuQuery);

            MenuEntity menuAdd = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "菜单新增",
                PermissionCode = "system:menu:add",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = menu.Id,
                IsDeleted = false
            };
            entities.Add(menuAdd);

            MenuEntity menuEdit = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "菜单修改",
                PermissionCode = "system:menu:edit",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = menu.Id,
                IsDeleted = false
            };
            entities.Add(menuEdit);

            MenuEntity menuRemove = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "菜单删除",
                PermissionCode = "system:menu:remove",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = menu.Id,
                IsDeleted = false
            };
            entities.Add(menuRemove);

            //部门管理
            MenuEntity dept = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "部门管理",
                PermissionCode = "system:dept:list",
                MenuType = MenuTypeEnum.Menu,
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
            entities.Add(dept);

            MenuEntity deptQuery = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "部门查询",
                PermissionCode = "system:dept:query",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = dept.Id,
                IsDeleted = false
            };
            entities.Add(deptQuery);

            MenuEntity deptAdd = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "部门新增",
                PermissionCode = "system:dept:add",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = dept.Id,
                IsDeleted = false
            };
            entities.Add(deptAdd);

            MenuEntity deptEdit = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "部门修改",
                PermissionCode = "system:dept:edit",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = dept.Id,
                IsDeleted = false
            };
            entities.Add(deptEdit);

            MenuEntity deptRemove = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "部门删除",
                PermissionCode = "system:dept:remove",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = dept.Id,
                IsDeleted = false
            };
            entities.Add(deptRemove);



            //岗位管理
            MenuEntity post = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "岗位管理",
                PermissionCode = "system:post:list",
                MenuType = MenuTypeEnum.Menu,
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
            entities.Add(post);

            MenuEntity postQuery = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "岗位查询",
                PermissionCode = "system:post:query",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = post.Id,
                IsDeleted = false
            };
            entities.Add(postQuery);

            MenuEntity postAdd = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "岗位新增",
                PermissionCode = "system:post:add",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = post.Id,
                IsDeleted = false
            };
            entities.Add(postAdd);

            MenuEntity postEdit = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "岗位修改",
                PermissionCode = "system:post:edit",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = post.Id,
                IsDeleted = false
            };
            entities.Add(postEdit);

            MenuEntity postRemove = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "岗位删除",
                PermissionCode = "system:post:remove",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = post.Id,
                IsDeleted = false
            };
            entities.Add(postRemove);

            //字典管理
            MenuEntity dict = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "字典管理",
                PermissionCode = "system:dict:list",
                MenuType = MenuTypeEnum.Menu,
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
            entities.Add(dict);

            MenuEntity dictQuery = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "字典查询",
                PermissionCode = "system:dict:query",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = dict.Id,
                IsDeleted = false
            };
            entities.Add(dictQuery);

            MenuEntity dictAdd = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "字典新增",
                PermissionCode = "system:dict:add",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = dict.Id,
                IsDeleted = false
            };
            entities.Add(dictAdd);

            MenuEntity dictEdit = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "字典修改",
                PermissionCode = "system:dict:edit",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = dict.Id,
                IsDeleted = false
            };
            entities.Add(dictEdit);

            MenuEntity dictRemove = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "字典删除",
                PermissionCode = "system:dict:remove",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = dict.Id,
                IsDeleted = false
            };
            entities.Add(dictRemove);


            //参数设置
            MenuEntity config = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "参数设置",
                PermissionCode = "system:config:list",
                MenuType = MenuTypeEnum.Menu,
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
            entities.Add(config);

            MenuEntity configQuery = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "参数查询",
                PermissionCode = "system:config:query",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = config.Id,
                IsDeleted = false
            };
            entities.Add(configQuery);

            MenuEntity configAdd = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "参数新增",
                PermissionCode = "system:config:add",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = config.Id,
                IsDeleted = false
            };
            entities.Add(configAdd);

            MenuEntity configEdit = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "参数修改",
                PermissionCode = "system:config:edit",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = config.Id,
                IsDeleted = false
            };
            entities.Add(configEdit);

            MenuEntity configRemove = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "参数删除",
                PermissionCode = "system:config:remove",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = config.Id,
                IsDeleted = false
            };
            entities.Add(configRemove);




            //日志管理
            MenuEntity log = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "日志管理",
                MenuType = MenuTypeEnum.Catalogue,
                Router = "log",
                IsShow = true,
                IsLink = false,
                MenuIcon = "log",
                OrderNum = 100,
                ParentId = system.Id,
                IsDeleted = false
            };
            entities.Add(log);

            //操作日志
            MenuEntity operationLog = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "操作日志",
                PermissionCode = "monitor:operlog:list",
                MenuType = MenuTypeEnum.Menu,
                Router = "operlog",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "monitor/operlog/index",
                MenuIcon = "form",
                OrderNum = 100,
                ParentId = log.Id,
                IsDeleted = false
            };
            entities.Add(operationLog);

            MenuEntity operationLogQuery = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "操作查询",
                PermissionCode = "monitor:operlog:query",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = operationLog.Id,
                IsDeleted = false
            };
            entities.Add(operationLogQuery);

            MenuEntity operationLogRemove = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "操作删除",
                PermissionCode = "monitor:operlog:remove",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = operationLog.Id,
                IsDeleted = false
            };
            entities.Add(operationLogRemove);


            //登录日志
            MenuEntity loginLog = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "登录日志",
                PermissionCode = "monitor:logininfor:list",
                MenuType = MenuTypeEnum.Menu,
                Router = "logininfor",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "monitor/logininfor/index",
                MenuIcon = "logininfor",
                OrderNum = 100,
                ParentId = log.Id,
                IsDeleted = false
            };
            entities.Add(loginLog);

            MenuEntity loginLogQuery = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "登录查询",
                PermissionCode = "monitor:logininfor:query",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = loginLog.Id,
                IsDeleted = false
            };
            entities.Add(loginLogQuery);

            MenuEntity loginLogRemove = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "登录删除",
                PermissionCode = "monitor:logininfor:remove",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = loginLog.Id,
                IsDeleted = false
            };
            entities.Add(loginLogRemove);
            return entities;
        }
    }
}
