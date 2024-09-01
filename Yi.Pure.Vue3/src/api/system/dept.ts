import {http} from "@/utils/http";
import type {ResultPage} from "@/api/result";

/** 获取系统管理-部门管理列表 */
export const getDeptList = (data?: object) => {
  return http.request<ResultPage>("get", "/dept", { data });
};
