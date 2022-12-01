using SqlSugar;
using System.Threading.Tasks;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class CommentService : BaseService<CommentEntity>, ICommentService
    {
        //添加一个评论
        public async Task<bool> AddAsync(CommentEntity comment)
        {
            //如果是一级评论：不用处理

            //如果是二级评论：ParentId父节点评论数+1
            return await _repository.UseTranAsync(async () =>
              {
                  if (comment.ParentId != 0)
                  {
                      var parentData = await _repository.GetByIdAsync(comment.ParentId);
                      parentData.CommentNum += 1;
                      await _repository.AsUpdateable(parentData).UpdateColumns(u => new { u.CommentNum }).ExecuteCommandAsync();
                  }
                  await _repository.InsertReturnSnowflakeIdAsync(comment);
              });

        }
    }
}
