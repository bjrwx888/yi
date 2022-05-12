import store from '../store/index'
//匹配按钮，判断是否有按钮存在
const btnDic =
{
    "user:get": "/",
    "user:add": "/",
    "user:update": "/",
    "user:del": "/",
}


 function getBtn(par) {
     const per=[];
    switch(par){
        case "user":
         



        break;
        default:console.log("未发现合法路由")
    }
}


export default { menuDic, btnDic };
