import myaxios from '@/utils/request'

//获取配置
export function getAll(){
    return myaxios({
        url: '/config',
        method: 'get'
    })
};