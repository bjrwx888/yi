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
        public static void UseDbSeedInitService(this IApplicationBuilder app)
        {

            if (Appsettings.appBool("DbSeed_Enabled"))
            {
              
                var _Db = app.ApplicationServices.GetService<ISqlSugarClient>();
                var users = SeedFactory.GetUserSeed();
                var roles = SeedFactory.GetRoleSeed();
                var menus = SeedFactory.GetMenuSeed();
                var dicts= SeedFactory.GetDictionarySeed();
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

                if (!_Db.Queryable<UserRoleEntity>().Any())
                {
                    _Db.Insertable(SeedFactory.GetUserRoleSeed(users, roles)).ExecuteCommand();
                }

                if (!_Db.Queryable<RoleMenuEntity>().Any())
                {
                    _Db.Insertable(SeedFactory.GetRoleMenuSeed(roles, menus)).ExecuteCommand();
                }


            }

        }
    }
}
