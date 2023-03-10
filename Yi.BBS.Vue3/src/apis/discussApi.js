import myaxios from '@/utils/request'
export function getList(data){
    return myaxios({
        url: '/discuss',
        method: 'get',
        params:data
    })
};