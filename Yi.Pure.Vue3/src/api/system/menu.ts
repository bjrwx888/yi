import {http} from "@/utils/http";
import type {ResultPage} from "@/api/result";

/** 查询菜单下拉树结构 */
export const getMenuOption = () => {
  return http.request<ResultPage>("get", `/menu`, {});
};
