import request from '@/utils/request'
// code to web
export function codeToWeb() {
  return request({
    url: 'code-gun/code-build-web',
    method: 'post'
  })
}
// code to web
export function webToCode(ids) {
  return request({
    url: 'code-gun/web-build-code',
    method: 'post',
    data:ids
  })
}

// open zhe path
export function openPath(path) {
  return request({
    url: `code-gun/dir/${encodeURIComponent(path)}`,
    method: 'post'
  })
}
