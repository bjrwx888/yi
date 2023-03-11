import myaxios from '@/utils/request'
export function getList(data){
    return myaxios({
        url: '/discuss',
        method: 'get',
        params:data
    })
};

export function getListByPlateId(plateId){
    return myaxios({
        url: `/discuss/plate-id/${plateId}`,
        method: 'get'
    })
};