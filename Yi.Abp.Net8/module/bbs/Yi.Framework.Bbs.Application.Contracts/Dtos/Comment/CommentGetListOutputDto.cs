using Volo.Abp.Application.Dtos;
using Yi.Framework.Rbac.Application.Contracts.Dtos.User;

namespace Yi.Framework.Bbs.Application.Contracts.Dtos.Comment
{
    /// <summary>
    /// ���۶෴
    /// </summary>
    public class CommentGetListOutputDto : EntityDto<Guid>
    {

        public DateTime? CreationTime { get; set; }




        public string Content { get; set; }


        /// <summary>
        /// ����id
        /// </summary>
        public Guid DiscussId { get; set; }

        public Guid ParentId { get; set; }

        public Guid RootId { get; set; }

        /// <summary>
        /// �û�,�������û���Ϣ
        /// </summary>
        public UserGetOutputDto CreateUser { get; set; }

        /// <summary>
        /// �����۵��û���Ϣ
        /// </summary>
        public UserGetOutputDto CommentedUser { get; set; }


        /// <summary>
        /// �������һ�����Σ����Ǵ���һ����ά���飬��Childrenֻ���ڶ���ʱ��ֻ��һ��
        /// </summary>
        public List<CommentGetListOutputDto> Children { get; set; } = new List<CommentGetListOutputDto>();
    }
}