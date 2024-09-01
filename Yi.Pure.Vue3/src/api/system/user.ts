import { http } from "@/utils/http";
import type { Result, ResultList, ResultPage } from "@/api/result";

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

/** 新增用户 */
export const addUser = (data: any) => {
  return http.request<Result>("post", `/user`, { data });
};
