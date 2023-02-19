import myaxios from '@/utils/myaxios'
export default {
    getComments() {
        return myaxios({
            url: '/Comment',
            method: 'get'
        })
    },
    getCommentsByDiscussId(discussId, pageIndex) {
        return myaxios({
            url: `/Comment/discuss-id/${discussId}?pageIndex=${pageIndex}&pageSize=10`,
            method: 'get'
        })
    },
    addComment(Comment, discussId) {
        return myaxios({
            url: `/Comment`,
            method: 'post',
            data: {
                content:Comment.content,discussId
            }
        })
    },
    updateComment(Comment) {
        return myaxios({
            url: '/Comment/UpdateComment',
            method: 'post',
            data: Comment
        })
    },
    delCommentList(Ids) {
        return myaxios({
            url: '/Comment/DelCommentList',
            method: 'post',
            data: Ids
        })
    },
}