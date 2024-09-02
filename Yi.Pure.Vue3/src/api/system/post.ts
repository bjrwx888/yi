import { http } from "@/utils/http";
import type { Result, ResultPage } from "@/api/result";

/** 查询岗位列表 */
export const getList = (data?: object) => {
  return http.request<ResultPage>("get", "/dept", { data });
};

/** 查询部门详细 */
export const getDept = id => {
  return http.request<Result>("get", `/dept/${id}`, {});
};

/** 新增部门 */
export const addDept = data => {
  return http.request<Result>("post", `/dept`, { data });
};

/** 修改部门 */
export const updateDept = (id, data) => {
  return http.request<Result>("put", `/dept/${id}`, { data });
};

/** 删除部门 */
export const delDept = id => {
  return http.request<Result>("delete", `/dept`, { params: { id } });
};

/** 获取部门选择框列表 */
export const getPostOptionSelect = () => {
  return http.request<Result>("get", `/post`, {});
};
