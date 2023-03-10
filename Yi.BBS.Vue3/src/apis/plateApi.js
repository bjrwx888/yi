import myaxios from '@/utils/request'
export function getList(data){
    return myaxios({
        url: '/plate',
        method: 'get',
        params:data
    })
};