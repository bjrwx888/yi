import myaxios from '@/util/myaxios'
export default {
    SetRoleByUser(userIds, roleIds) {
        return myaxios({
            url: '/User/SetRoleByUser',
            method: 'post',
            data: { "ids1": userIds, "ids2": roleIds }
        })
    },



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