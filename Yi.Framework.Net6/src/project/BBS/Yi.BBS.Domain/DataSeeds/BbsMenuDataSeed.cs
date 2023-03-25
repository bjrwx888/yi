using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Data.DataSeeds;
using Yi.Framework.Ddd.Repositories;
using Yi.RBAC.Domain.Identity.Entities;
using Yi.RBAC.Domain.Shared.Identity.EnumClasses;

namespace Yi.BBS.Domain.DataSeed
{
    [AppService(typeof(IDataSeed))]
    public class BbsMenuDataSeed : AbstractDataSeed<MenuEntity>
    {
        public BbsMenuDataSeed(IRepository<MenuEntity> repository) : base(repository)
        {
        }

        public override async Task<bool> IsInvoker()
        {
            return !await _repository.IsAnyAsync(x => x.MenuName == "BBS");
        }
        public override List<MenuEntity> GetSeedData()
        {
            List<MenuEntity> entities = new List<MenuEntity>();

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


            //评论管理
            MenuEntity comment = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "评论管理",
                PermissionCode = "bbs:comment:list",
                MenuType = MenuTypeEnum.Menu,
                Router = "comment",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "bbs/comment/index",
                MenuIcon = "education",
                OrderNum = 100,
                ParentId = bbs.Id,
                IsDeleted = false
            };
            entities.Add(comment);

            MenuEntity commentQuery = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "评论查询",
                PermissionCode = "bbs:comment:query",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = comment.Id,
                IsDeleted = false
            };
            entities.Add(commentQuery);

            MenuEntity commentAdd = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "评论新增",
                PermissionCode = "bbs:comment:add",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = comment.Id,
                IsDeleted = false
            };
            entities.Add(commentAdd);

            MenuEntity commentEdit = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "评论修改",
                PermissionCode = "bbs:comment:edit",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = comment.Id,
                IsDeleted = false
            };
            entities.Add(commentEdit);

            MenuEntity commentRemove = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "评论删除",
                PermissionCode = "bbs:comment:remove",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = comment.Id,
                IsDeleted = false
            };
            entities.Add(commentRemove);


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


            //主题管理
            MenuEntity discuss = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "主题管理",
                PermissionCode = "bbs:discuss:list",
                MenuType = MenuTypeEnum.Menu,
                Router = "discuss",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "bbs/discuss/index",
                MenuIcon = "education",
                OrderNum = 100,
                ParentId = bbs.Id,
                IsDeleted = false
            };
            entities.Add(discuss);

            MenuEntity discussQuery = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "主题查询",
                PermissionCode = "bbs:discuss:query",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = discuss.Id,
                IsDeleted = false
            };
            entities.Add(discussQuery);

            MenuEntity discussAdd = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "主题新增",
                PermissionCode = "bbs:discuss:add",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = discuss.Id,
                IsDeleted = false
            };
            entities.Add(discussAdd);

            MenuEntity discussEdit = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "主题修改",
                PermissionCode = "bbs:discuss:edit",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = discuss.Id,
                IsDeleted = false
            };
            entities.Add(discussEdit);

            MenuEntity discussRemove = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "主题删除",
                PermissionCode = "bbs:discuss:remove",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = discuss.Id,
                IsDeleted = false
            };
            entities.Add(discussRemove);



            //板块管理
            MenuEntity plate = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "板块管理",
                PermissionCode = "bbs:plate:list",
                MenuType = MenuTypeEnum.Menu,
                Router = "plate",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "bbs/plate/index",
                MenuIcon = "education",
                OrderNum = 100,
                ParentId = bbs.Id,
                IsDeleted = false
            };
            entities.Add(plate);

            MenuEntity plateQuery = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "板块查询",
                PermissionCode = "bbs:plate:query",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = plate.Id,
                IsDeleted = false
            };
            entities.Add(plateQuery);

            MenuEntity plateAdd = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "板块新增",
                PermissionCode = "bbs:plate:add",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = plate.Id,
                IsDeleted = false
            };
            entities.Add(plateAdd);

            MenuEntity plateEdit = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "板块修改",
                PermissionCode = "bbs:plate:edit",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = plate.Id,
                IsDeleted = false
            };
            entities.Add(plateEdit);

            MenuEntity plateRemove = new MenuEntity()
            {
                Id = SnowflakeHelper.NextId,
                MenuName = "板块删除",
                PermissionCode = "bbs:plate:remove",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = plate.Id,
                IsDeleted = false
            };
            entities.Add(plateRemove);

            //默认值
            entities.ForEach(m =>
            {
                m.IsDeleted = false;
                m.State = true;
            });
            return entities;
        }
    }
}
