using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Model.Models;
using Yi.Framework.Model.SeedData;

namespace Yi.Framework.WebCore.DbExtend
{
    public static class DbSeedExtend
    {
        public static bool Invoer(ISqlSugarClient _Db)
        {
            bool res = false;
            var users = SeedFactory.GetUserSeed();
            var roles = SeedFactory.GetRoleSeed();
            var menus = SeedFactory.GetMenuSeed();
            var dicts = SeedFactory.GetDictionarySeed();
            var posts = SeedFactory.GetPostSeed();
            var dictinfos = SeedFactory.GetDictionaryInfoSeed();
            var depts = SeedFactory.GetDeptSeed();
            var files = SeedFactory.GetFileSeed();
            try
            {
                _Db.AsTenant().BeginTran();

                if (!_Db.Queryable<UserEntity>().Any())
                {
                    _Db.Insertable(users).ExecuteCommand();
                }

                if (!_Db.Queryable<RoleEntity>().Any())
                {
                    _Db.Insertable(roles).ExecuteCommand();
                }

                if (!_Db.Queryable<MenuEntity>().Any())
                {
                    _Db.Insertable(menus).ExecuteCommand();
                }

                if (!_Db.Queryable<DictionaryEntity>().Any())
                {
                    _Db.Insertable(dicts).ExecuteCommand();
                }
                if (!_Db.Queryable<PostEntity>().Any())
                {
                    _Db.Insertable(posts).ExecuteCommand();
                }
                if (!_Db.Queryable<DictionaryInfoEntity>().Any())
                {
                    _Db.Insertable(dictinfos).ExecuteCommand();
                }
                if (!_Db.Queryable<DeptEntity>().Any())
                {
                    _Db.Insertable(depts).ExecuteCommand();
                }

                if (!_Db.Queryable<UserRoleEntity>().Any())
                {
                    _Db.Insertable(SeedFactory.GetUserRoleSeed(users, roles)).ExecuteCommand();
                }

                if (!_Db.Queryable<RoleMenuEntity>().Any())
                {
                    _Db.Insertable(SeedFactory.GetRoleMenuSeed(roles, menus)).ExecuteCommand();
                }

                if (!_Db.Queryable<FileEntity>().Any())
                {
                    _Db.Insertable(files).ExecuteCommand();
                }

                _Db.AsTenant().CommitTran();
                res = true;
            }
            catch (Exception ex)
            {
                _Db.AsTenant().RollbackTran();//数据回滚
                Console.WriteLine(ex);

            }
            return res;
        }

        public static void UseDbSeedInitService(this IApplicationBuilder app)
        {


            if (Appsettings.appBool("DbSeed_Enabled"))
            {
                var _Db = app.ApplicationServices.GetRequiredService<ISqlSugarClient>();
                Invoer(_Db);
            }

        }
    }
}
