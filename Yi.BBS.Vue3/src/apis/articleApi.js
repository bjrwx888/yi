import myaxios from '@/utils/request'
export function getList(data){
    return myaxios({
        url: '/article',
        method: 'get',
        params:data
    })
};
export function get(id){
    return myaxios({
        url: `/article/${id}`,
        method: 'get'
    })
};
export function add(data){
    return myaxios({
        url: `/article`,
        method: 'post',
        data:data
    })
};
export function update(id,data){
    return myaxios({
        url: `/article/${id}`,
        method: 'put',
        data:data
    })
};
export function del(ids){
    return myaxios({
        url: `/article/${ids}`,
        method: 'delete'
    })
};
export function all(discussId)
{
    return myaxios({
        url: `/article/all/discuss-id/${discussId}`,
        method: 'get'
    })
}