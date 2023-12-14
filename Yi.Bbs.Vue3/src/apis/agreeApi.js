import myaxios from '@/utils/request'
export function operate(discussId){
    if(discussId==undefined)
    {
        return;
    }
    return myaxios({
        url: `/agree/operate/${discussId}`,
        method: 'post'
    })
};