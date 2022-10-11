import myaxios from '@/utils/myaxios.ts'
import { ArticleEntity } from '@/type/interface/ArticleEntity'

export default {
    add(data: ArticleEntity) {
        console.log(data)
        return myaxios({
            url: `/article/add`,
            method: 'post',
            data: data
        })
    },
    pageList(data:ArticleEntity) {
        return myaxios({
          url: '/article/pageList',
          method: 'get',
          params: data
        })
      }
}