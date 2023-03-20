import myaxios from '@/utils/request'
export function getList(data){
    return myaxios({
        url: '/banner',
        method: 'get',
        params:data
    })
};