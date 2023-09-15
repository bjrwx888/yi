import request from '@/utils/request'  
// 触发访问
export function access() {
  return request({
    url: '/access-log',
    method: 'post'
  })
}


// 获取本周数据
export function getWeek() {
  return request({
    url: '/access-log/week',
    method: 'get'
  })
}