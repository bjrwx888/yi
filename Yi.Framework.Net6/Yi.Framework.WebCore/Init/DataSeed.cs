using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Model.ModelFactory;
using Yi.Framework.Model.Models;

namespace Yi.Framework.WebCore.Init
{
    public class DataSeed
    {
        public async static Task SeedAsync(IDbContextFactory _DbFactory)
        {
            var _Db = _DbFactory.ConnWriteOrRead(Common.Enum.WriteAndReadEnum.Write);
            if (!_Db.Set<user>().Any())
            {
                List<menu> menus = new List<menu>{

                new  menu{ id=1, menu_name="根",is_show=1,is_top=1},
                new  menu{ id=2,icon="mdi-view-dashboard", menu_name="首页",is_show=1,is_top=0,router="/",parentId=1},
                new  menu{id=3,icon="mdi-account-box-multiple", menu_name="用户角色管理",is_show=1,is_top=0,parentId=1},

                 new  menu{id=4,icon="mdi-account-box", menu_name="用户管理",router="/AdmUser/",is_show=1,is_top=0,parentId=3},
                 new  menu{id=5, menu_name="get",is_show=0,is_top=0,parentId=4,mould=new mould{mould_name="get",url="/user/getuser" } },
                 new  menu{id=6, menu_name="update",is_show=0,is_top=0,parentId=4,mould=new mould{mould_name="update",url="/user/updateuser" } },
                 new  menu{id=7, menu_name="del",is_show=0,is_top=0,parentId=4,mould=new mould{mould_name="del",url="/user/dellistUser" } },
                 new  menu{id=8, menu_name="add",is_show=0,is_top=0,parentId=4,mould=new mould{mould_name="add",url="/role/adduser" } },

                 new  menu{ id=9,icon="mdi-account-circle", menu_name="角色管理",router="/admrole/",is_show=1,is_top=0,parentId=3},
                 new  menu{id=10, menu_name="get",is_show=0,is_top=0,parentId=9,mould=new mould{mould_name="get",url="/role/getrole" } },
                 new  menu{id=11, menu_name="update",is_show=0,is_top=0,parentId=9,mould=new mould{mould_name="update",url="/role/updaterole" } },
                 new  menu{id=12, menu_name="del",is_show=0,is_top=0,parentId=9,mould=new mould{mould_name="del",url="/role/dellistrole" } },
                 new  menu{id=13, menu_name="add",is_show=0,is_top=0,parentId=9,mould=new mould{mould_name="add",url="/role/addrole" } },


                 new  menu{ id=14,icon="mdi-account-cash", menu_name="角色接口管理",is_show=1,is_top=0,parentId=1},


                 new  menu{ id=15,icon="mdi-clipboard-check-multiple", menu_name="菜单管理",router="/AdmMenu/",is_show=1,is_top=0,parentId=14},
                 new  menu{id=16, menu_name="get",is_show=0,is_top=0,parentId=15,mould=new mould{mould_name="get",url="/menu/getmenu" } },
                 new  menu{id=17, menu_name="update",is_show=0,is_top=0,parentId=15,mould=new mould{mould_name="update",url="/menu/updatemenu" } },
                 new  menu{id=18, menu_name="del",is_show=0,is_top=0,parentId=15,mould=new mould{mould_name="del",url="/menu/dellistmenu" } },
                 new  menu{id=19, menu_name="add",is_show=0,is_top=0,parentId=15,mould=new mould{mould_name="add",url="/menu/addmenu" } },



                new  menu{ id=20,icon="mdi-circle-slice-8", menu_name="接口管理",router="/admMould/",is_show=1,is_top=0,parentId=14},
                 new  menu{id=21, menu_name="get",is_show=0,is_top=0,parentId=20,mould=new mould{mould_name="get",url="/Mould/getMould" } },
                 new  menu{id=22, menu_name="update",is_show=0,is_top=0,parentId=20,mould=new mould{mould_name="update",url="/Mould/updateMould" } },
                 new  menu{id=23, menu_name="del",is_show=0,is_top=0,parentId=20,mould=new mould{mould_name="del",url="/Mould/dellistMould" } },
                 new  menu{id=24, menu_name="add",is_show=0,is_top=0,parentId=20,mould=new mould{mould_name="add",url="/Mould/addMould" } },

                new  menu{ id=25,icon="mdi-clipboard-account", menu_name="角色菜单分配管理",router="/admRoleMenu/",is_show=1,is_top=0,parentId=14},

                new  menu{ id=26,icon="mdi-clipboard-flow-outline", menu_name="路由管理",is_show=1,is_top=0,parentId=1},
                new  menu{ id=27,icon="mdi-account-eye", menu_name="用户信息",router="/userinfo/",is_show=1,is_top=0,parentId=26},

                };


                List<role> roles = new List<role>() {
                new role(){role_name="普通用户" },
                new role(){role_name="管理员",menus= menus}
                };

                List<user> users = new List<user>() {
                new user(){ username="admin",password="123",roles=roles}
            };

                await _Db.Set<user>().AddRangeAsync(users);
                await _Db.SaveChangesAsync();
                Console.WriteLine(nameof(DbContext) + ":数据库初始成功！");
            }
        }
    }
}
