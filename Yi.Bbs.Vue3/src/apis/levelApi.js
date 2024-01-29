import request from "@/config/axios/service";

export function getList(data) {
  return request({
    url: "/level",
    method: "get",
    params: data,
  });
}
