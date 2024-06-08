import request from "@/config/axios/service";
export function GetResult() {
  return request({
    url: `http://ccnetcore.com:19002/api/app/nue-get-info/info`,
    method: "get",
  });
}
