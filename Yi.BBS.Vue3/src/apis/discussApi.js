import myaxios from '@/utils/request'
export function getList(data){
    return myaxios({
        url: '/discuss',
        method: 'get',
        params:data
    })
};
export function getTopList(data){

    if(data==undefined)
    {
        data={isTop:true}
    }
    else
    {
        data["isTop"]=true;
    }

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
export function add(data){
    return myaxios({
        url: `/discuss`,
        method: 'post',
        data:data
    })
};
export function update(id,data){
    return myaxios({
        url: `/discuss/${id}`,
        method: 'put',
        data:data
    })
};
export function del(ids){
    return myaxios({
        url: `/discuss/${ids}`,
        method: 'delete'
    })
};