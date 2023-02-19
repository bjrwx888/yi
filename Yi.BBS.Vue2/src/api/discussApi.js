import myaxios from '@/utils/myaxios'
export default {
    getDiscuss() {
        return myaxios({
            url: '/Discuss',
            method: 'get'
        })
    },
    getDiscussByPlateId(plateId, pageIndex, orderbyId) {
        return myaxios({
            url: `/discuss/plate-id/${plateId}?&pageIndex=${pageIndex}&pageSize=10`,//&orderbyId=${orderbyId}
            method: 'get'
        })
    },
    getDiscussByUserId(userId, pageIndex) {
        if (userId == undefined) {
            userId = 0
        }
        return myaxios({
            url: `/Discuss/getDiscussByUserId?userId=${userId}&pageIndex=${pageIndex}`,
            method: 'get'
        })
    },


    addDiscuss(data, plateId, labelIds) {
        return myaxios({
            url: `/Discuss`,
            method: 'post',
            data: {
                content:data.content,
                introduction:data.introduction,
                title:data.title,
                types:data.type,
                plateId,
                // Ids
            }
        })
    },
    updateDiscuss(data) {
        return myaxios({
            url: '/Discuss/UpdateDiscuss',
            method: 'post',
            data: data
        })
    },
    delDiscussList(Ids) {
        return myaxios({
            url: '/Discuss/DelDiscussList',
            method: 'post',
            data: Ids
        })
    },
    getDiscussByDiscussId(id) {
        return myaxios({
            url: `/Discuss/${id}`,
            method: 'get'
        })
    },
    UpdatePorp(disucssId, propId, color) {
        color = color.replace("#", "%23"); //颜色代码替换
        return myaxios({
            url: `/Discuss/UpdatePorp?disucssId=${disucssId}&propId=${propId}&color=${color}`,
            method: 'get'
        })
    },


    GetPlateInfoBydiscussId(discussId)
{
    return myaxios({
        url: `/Discuss/GetPlateInfoBydiscussId?discussId=${discussId}`,
        method: 'get'
    })
}
}