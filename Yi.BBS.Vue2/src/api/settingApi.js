import myaxios from '@/utils/myaxios'
export default {
    getTitle() {
        return myaxios({
            url: '/Setting/title',
            method: 'get'
        })
    },
    getSetting() {
        return myaxios({
            url: '/Setting/getSetting',
            method: 'get'
        })
    },
    UpdateSetting(form) {
        return myaxios({
            url: '/Setting/UpdateSetting',
            method: 'post',
            data: form
        })
    }
}