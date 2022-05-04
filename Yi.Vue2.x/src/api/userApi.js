import myaxios from '@/util/myaxios'
export default {

    GetUserInRolesByHttpUser() {
        return myaxios({
            url: `/User/GetUserInRolesByHttpUser`,
            method: 'get'
        })
    },
    GetMenuByHttpUser() {
        return myaxios({
            url: `/User/GetMenuByHttpUser`,
            method: 'get'
        })
    },
    GetRoleListByUserId(userId) {
        return myaxios({
            url: `/User/GetRoleListByUserId?userId=${userId}`,
            method: 'get'
        })
    },
}