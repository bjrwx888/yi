//匹配菜单，让code变成路由
const menuDic=
{
    "user:get:list": "/admuser", 
    "role:get:list": "/admrole",
    "menu:get:list":"/admmenu",
    "rolemenu:set:list":"/admrolemenu"
} 
//匹配按钮，判断是否有按钮存在
const btnDic=
{
   "user:add":"",
   "user:update":"",
   "user:del":"",
}
export default {menuDic,btnDic};
//菜单可以区分使用code来进行匹配
//记得：关于*的使用，要单独判断
//比如，

//按钮是user:*或者*:*:*直接全部放行即可

//菜单就不一样了,如果是*:*:*
//有两种方案：
//1:直接使用一个默认的全部菜单（会和后端给的菜单冲突）
//2:前端直接无视，*:*:*相当于只管后端权限（如果后端没有配置菜单前端将没有菜单了）


//如果查询找到的是user:*，可以先把*全部替换成get:list再进行比对即可
