import request from "@/config/axios/service";

export function getListByDiscussId(discussId, data) {
  return request({
    url: `/comment/discuss-id/${discussId}`,
    method: "get",
    params: data,
  });
}
export function add(data) {
  return request({
    url: `/comment`,
    method: "post",
    data: data,
  });
}

export function del(ids) {
  return request({
    url: `/comment/${ids}`,
    method: "delete",
  });
}
