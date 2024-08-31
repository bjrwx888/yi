import { http } from "@/utils/http";
import type { Result, ResultList, ResultPage } from "./result.ts";

/** 获取系统管理-用户管理列表 */
export const getUserList = (data?: object) => {
  return http.request<ResultPage>("get", "/user", { params: data });
};

/** 获取一个用户详细消息 */
export const getUser = (userId: string) => {
  return http.request<Result>("get", `/user/${userId}`, {});
};

/** 删除用户 */
export const delUser = (userIds: string[]) => {
  return http.request<Result>("delete", `/user`, {
    params: { id: userIds }
  });
};

/** 用户密码重置 */
export const resetUserPwd = (id: string, password: string) => {
  return http.request<Result>("put", `/account/rest-password/${id}`, {
    data: { password }
  });
};

/** 改变用户状态 */
export const changeUserStatus = (userId: string, state: boolean) => {
  return http.request<Result>("put", `/user/${userId}/${state}`, {});
};

/** 修改用户 */
export const updateUser = (id: string, data: any) => {
  return http.request<Result>("put", `/user/${id}`, { data });
};

/** 获取角色选择框列表 */
export const getRoleOption = () => {
  return http.request<ResultPage>("get", `/role`, {});
};

/** 新增角色 */
export const addUser = (data: any) => {
  return http.request<Result>("post", `/user`, { data });
};

/** 新增角色 */
export const addRole = (data: any) => {
  return http.request<Result>("post", `/role`, { data });
};

/** 获取系统管理-角色管理列表 */
export const getRoleList = (data?: object) => {
  return http.request<ResultPage>("post", "/role", { data });
};

/** 获取系统管理-菜单管理列表 */
export const getMenuList = (data?: object) => {
  return http.request<Result>("post", "/menu", { data });
};

/** 获取系统管理-部门管理列表 */
export const getDeptList = (data?: object) => {
  return http.request<ResultPage>("get", "/dept", { data });
};

/** 获取系统监控-在线用户列表 */
export const getOnlineLogsList = (data?: object) => {
  return http.request<ResultPage>("post", "/online-logs", { data });
};

/** 获取系统监控-登录日志列表 */
export const getLoginLogsList = (data?: object) => {
  return http.request<ResultPage>("post", "/login-logs", { data });
};

/** 获取系统监控-操作日志列表 */
export const getOperationLogsList = (data?: object) => {
  return http.request<ResultPage>("post", "/operation-logs", { data });
};

/** 获取系统监控-系统日志列表 */
export const getSystemLogsList = (data?: object) => {
  return http.request<ResultPage>("post", "/system-logs", { data });
};

/** 获取系统监控-系统日志-根据 id 查日志详情 */
export const getSystemLogsDetail = (data?: object) => {
  return http.request<ResultList>("post", "/system-logs-detail", { data });
};

/** 获取角色管理-权限-菜单权限 */
export const getRoleMenu = (data?: object) => {
  return http.request<ResultList>("post", "/role-menu", { data });
};

/** 获取角色管理-权限-菜单权限-根据角色 id 查对应菜单 */
export const getRoleMenuIds = (data?: object) => {
  return http.request<ResultList>("post", "/role-menu-ids", { data });
};
