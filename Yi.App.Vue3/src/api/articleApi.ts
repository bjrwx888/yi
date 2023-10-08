import myaxios from '@/utils/myaxios'

export default {
    add(data:any) {
        return myaxios({
            url: `/Trends`,
            method: 'post',
            data: data
        })
    },
    pageList(data:any) {
        return myaxios({
          url: '/Trends',
          method: 'get',
          params: data
        })
      }
}