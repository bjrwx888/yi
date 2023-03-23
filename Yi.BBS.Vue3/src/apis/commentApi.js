import myaxios from '@/utils/request'
export function getListByDiscussId(discussId,data){
    return myaxios({
        url: `/comment/discuss-id/${discussId}`,
        method: 'get',
        params:data
    })
};
export function add(data){
    return myaxios({
        url: `/comment`,
        method: 'post',
        data:data
    })
};