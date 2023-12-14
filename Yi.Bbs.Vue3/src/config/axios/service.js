import axios from "axios";
import { ElMessage } from "element-plus";
import { config } from "@/config/axios/config";
import { Session } from "@/utils/storage";
import useAuths from "@/hooks/useAuths";

const { getToken } = useAuths();

const { request_timeout } = config;
export const PATH_URL = import.meta.env.VITE_APP_BASEAPI;

// 配置新建一个 axios 实例
const service = axios.create({
  baseURL: PATH_URL, // api 的 base_url
  timeout: request_timeout, // 请求超时时间
  headers: { "Content-Type": "application/json" },
  hideerror: false, //是否在底层显示错误信息
});

// 添加请求拦截器
service.interceptors.request.use(
  (config) => {
    // 在发送请求之前做些什么 token
    const token = getToken();
    if (token) {
      config.headers["Authorization"] = `Bearer ${token}`;
    }
    if (Session.get("tenantId")) {
      config.headers["TenantId"] = Session.get("tenantId");
    }
    return config;
  },
  (error) => {
    // 对请求错误做些什么
    return Promise.reject(error);
  }
);

// 添加响应拦截器
service.interceptors.response.use(
  (response) => {
    return Promise.resolve(response);
  },
  (error) => {
    // 对响应错误做点什么
    if (error.message.indexOf("timeout") != -1) {
      ElMessage({
        type: "danger",
        message: "网络超时",
      });
    } else if (error.message == "Network Error") {
      ElMessage({
        type: "danger",
        message: "网络连接错误",
      });
    } else {
      const res = error.response || {};
      const status = Number(res.status) || 200;
      const message = res.data.error.message;
      if (status === 401) {
        ElMessage({
          type: "danger",
          message,
        });
        return;
      }
      if (status !== 200) {
        if (status >= 500) {
          ElMessage({
            type: "danger",
            message: "网络开小差了，请稍后再试",
          });
          return Promise.reject(new Error(message));
        }
        // 避开找不到后端接口的提醒
        if (status !== 404) {
          ElMessage({
            type: "danger",
            message,
          });
        }
      }
    }
    return Promise.reject(new Error(error));
  }
);

// 导出 axios 实例
export default service;
