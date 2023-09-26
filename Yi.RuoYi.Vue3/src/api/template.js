import request from '@/utils/request'
/* 以下为api的模板，通用的crud，将以下变量替换即可：
@model@ : 实体模型
*/
// 分页查询
export function listData(query) {
  return request({
    url: '/@model@',
    method: 'get',
    params: query
  })
}

// id查询
export function getData(id) {
  return request({
    url: `/@model@/${id}`,
    method: 'get'
  })
}

// 新增
export function addData(data) {
  return request({
    url: '/@model@',
    method: 'post',
    data: data
  })
}

// 修改
export function updateData(id,data) {
  return request({
    url: `/@model@/${id}`,
    method: 'put',
    data: data
  })
}

// 删除
export function delData(ids) {
  return request({
    url: `/@model@/${ids}`,
    method: 'delete',
  })
}
