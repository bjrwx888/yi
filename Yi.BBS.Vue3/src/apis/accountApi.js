import myaxios from '@/utils/request'
export function login(username, password){
    return myaxios({
        url: '/Account/login',
        method: 'post',
        data: {
            username,
            password
        }
    })
};