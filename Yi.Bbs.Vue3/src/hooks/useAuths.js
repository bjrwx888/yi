import { ElMessage, ElMessageBox } from "element-plus";
import useUserStore from "@/stores/user";
import router from "@/router";
import { Session, Local } from "@/utils/storage";
import { userLogin, getUserDetailInfo, userLogout } from "@/apis/auth";

const TokenKey = "AccessToken";
export const AUTH_MENUS = "AUTH_MENUS";
export const AUTH_USER = "AUTH_USER";

export default function useAuths(opt) {
  const defaultOpt = {
    loginUrl: "/login", // 登录页跳转url 默认: /login
    loginReUrl: "", // 登录页登陆成功后带重定向redirect=的跳转url 默认为空
    homeUrl: "/index", // 主页跳转url 默认: /index
    otherQuery: {}, // 成功登录后携带的（除redirect外）其他参数
  };

  let option = {
    ...defaultOpt,
    ...opt,
  };

  // 获取token
  const getToken = () => {
    return Session.get(TokenKey);
  };

  // 存储token到cookies
  const setToken = (token) => {
    if (token == null) {
      return false;
    }
    Session.set(TokenKey, token);
    return true;
  };

  // 删除token
  const removeToken = () => {
    Session.remove(TokenKey);
    return true;
  };

  // 退出登录
  const logoutFun = async () => {
    let flag = true;
    try {
      await userLogout().then((res) => {
        ElMessage({
          message: "退出成功",
          type: "info",
          duration: 2000,
        });
      });
    } catch (error) {
      flag = await ElMessageBox.confirm(
        `退出登录失败，是否强制退出？`,
        "提示",
        {
          confirmButtonText: "确 定",
          cancelButtonText: "取 消",
          type: "warning",
        }
      )
        .then(() => {
          return true;
        })
        .catch(() => {
          //取消
          return false;
        });
    }
    if (flag) {
      clearStorage();
    }
  };

  // 清空本地存储的信息
  const clearStorage = () => {
    Session.clear();
    Local.clear();
    removeToken();
  };

  // 用户名密码登录
  const loginFun = async (params) => {
    const res = await userLogin(params);
    ElMessage({
      message: `您好${params.userName}，登录成功！`,
      type: "success",
    });
    loginSuccess(res);
    return res;
  };

  // 获取用户基本信息、角色、菜单权限
  const getUserInfo = async () => {
    try {
      let { data } = await getUserDetailInfo();
      // useUserStore
      // store.dispatch("updateUserInfo", result);
      return data;
    } catch (error) {
      return {};
    }
  };

  // 登录成功之后的操作
  const loginSuccess = async (res) => {
    const { token } = res.data;

    setToken(token);
    try {
      // 存储用户信息
      await getUserInfo(); // 用户信息
      // 登录成功后 路由跳转
      // 如果有记录当前跳转页面
      const currentPath = Session.get("currentPath");
      if (currentPath) {
        router.push(currentPath);
      } else {
        router.replace({
          path: option.loginReUrl ? option.loginReUrl : option.homeUrl,
          query: option.otherQuery,
        });
      }
    } catch (error) {
      removeToken();
      return false;
    }
  };

  return {
    getToken,
    setToken,
    removeToken,
    loginFun,
    getUserInfo,
    logoutFun,
    clearStorage,
  };
}
