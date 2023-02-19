import myaxios from '@/utils/myaxios'
export default {
    setArticleByCache(articleId, content) {
        return myaxios({
            url: `/Article/setArticleByCache`,
            method: 'post',
            data: { id: articleId, content: content }
        })
    },

    getArticleByCache(articleId) {
        return myaxios({
            url: `/Article/getArticleByCache?articleId=${articleId}`,
            method: 'get'
        })
    },


    getArticlesByDiscussId(discussId) {
        return myaxios({
            url: `/Article/discuss-id/${discussId}`,
            method: 'get'
        })
    },
    getArticleById(articleId) {
        return myaxios({
            url: `/Article/${articleId}`,
            method: 'get'
        })

    },
    getTitlArticles(discussId) {
        return myaxios({
            url: `/article/all/discuss-id/${discussId}`,
            method: 'get'
        })
    },
    getArticles() {
        return myaxios({
            url: '/Article/getArticles',
            method: 'get'
        })
    },
    addChildrenArticle(article, parentId, discussId) {
        return myaxios({
            url: `/Article`,
            method: 'post',
            data: {
                content:article.content,
                name:article.name,
                discussId:discussId,
                parentId:parentId
            }
        })
    },
    addArticle(article, discussId) {
        return myaxios({
            url: `/Article`,
            method: 'post',
            data: {
                content:article.content,
                name:article.name,
                discussId:discussId,
                parentId:0
            }
        })
    },
    updateArticle(Article, discussId) {
        return myaxios({
            url: `/Article/UpdateArticle?discussId=${discussId}`,
            method: 'post',
            data: Article
        })
    },
    delArticleList(Ids, discussId) {
        console.log(Ids.join())
        return myaxios({
            url: `/Article/${Ids.join()}`,
            method: 'delete'
        })
    },
}