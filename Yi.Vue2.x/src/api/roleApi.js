import myaxios from '@/util/myaxios'
export default {
    getList() {
        return myaxios({
            url: '/Role/GetList',
            method: 'post',
            data: {
                parameters: [
                    {
                        key: "isDeleted",
                        value: "0",
                        type: 0

                    }
                ],
                orderBys: [
                    "id"
                ]
            }
        })
    },
    giveRoleSetMenu(roleList, menuList) {
        return myaxios({
            url: '/Role/GiveRoleSetMenu',
            method: 'put',
            data: { RoleIds: roleList, menuIds: menuList }
        })
    },

    getInMenuByRoleId(roleId) {
        return myaxios({
            url: `/Role/GetInMenuByRoleId?roleId=${roleId}`,
            method: 'get'
        })
    }

}