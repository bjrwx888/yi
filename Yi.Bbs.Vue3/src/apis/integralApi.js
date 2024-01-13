import request from "@/config/axios/service";

export function signIn() {
  return request({
    url: "/integral/sign-in",
    method: "post"
  });
}
