import { login, logout, getInfo, register } from "@/apis/accountApi";
import { getUserDetailInfo } from "@/apis/auth";
import useAuths from "@/hooks/useAuths";
import { defineStore } from "pinia";

const { getToken, setToken, removeToken } = useAuths();

const useUserStore = defineStore("user", {
  state: () => ({
    id: "",
    token: getToken(),
    name: "游客",
    userName: "",
    icon: null,
    roles: [],
    permissions: [],
  }),
  getters: {},
  actions: {
    // 登录
    login(userInfo) {
      const userName = userInfo.userName.trim();
      const password = userInfo.password;
      const code = userInfo.code;
      const uuid = userInfo.uuid;
      return new Promise((resolve, reject) => {
        login(userName, password, code, uuid)
          .then((response) => {
            const res = response.data;
            setToken(res.token);
            this.token = res.token;
            resolve(response);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },
    // 获取用户信息
    getInfo() {
      return new Promise((resolve, reject) => {
        getUserDetailInfo()
          .then((response) => {
            const res = response.data;

            const user = res.user;
            console.log(user, "user");
            const avatar =
              user.icon == "" || user.icon == null
                ? "/favicon.ico"
                : import.meta.env.VITE_APP_BASEAPI + "/file/" + user.icon;
            console.log(avatar, "avatar");
            if (res.roleCodes && res.roleCodes.length > 0) {
              // 验证返回的roles是否是一个非空数组
              this.roles = res.roleCodes;
              this.permissions = res.permissionCodes;
              // this.roles = ["admin"];
              // this.permissions=["*:*:*"]
            } else {
              this.roles = ["ROLE_DEFAULT"];
            }
            // this.roles = ["admin"];
            // this.permissions=["*:*:*"]
            this.name = user.nick;
            this.icon = avatar;

            this.userName = user.userName;
            this.id = user.id;
            resolve(res);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },
    // 退出系统
    logOut() {
      return new Promise((resolve, reject) => {
        logout()
          .then(() => {
            this.token = "";
            this.roles = [];
            this.permissions = [];
            removeToken();
            resolve();
          })
          .catch((error) => {
            reject(error);
          });
      });
    },
    // 注册
    register(userInfo) {
      const userName = userInfo.userName.trim();
      const password = userInfo.password.trim();
      const phone = userInfo.phone;
      const uuid = userInfo.uuid;
      const code = userInfo.code;
      return new Promise((resolve, reject) => {
        register(userName, password, phone, code, uuid)
          .then((response) => {
            resolve(response);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },
  },
});
export default useUserStore;
