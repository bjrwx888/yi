import myaxios from '@/utils/request'
export function upload(data){
    return myaxios({
        url: '/file',
        method: 'post',
        data:data,
        headers: { 'Content-Type': 'multipart/form-data' }
    })
};
