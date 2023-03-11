import myaxios from '@/utils/request'
export function getList(data){
    return myaxios({
        url: '/discuss',
        method: 'get',
        params:data
    })
};
export function get(id){
    return myaxios({
        url: `/discuss/${id}`,
        method: 'get'
    })
};
