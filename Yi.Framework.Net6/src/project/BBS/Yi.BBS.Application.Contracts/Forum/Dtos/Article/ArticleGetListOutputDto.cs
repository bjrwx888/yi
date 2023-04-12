using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Dtos;

namespace Yi.BBS.Application.Contracts.Forum.Dtos
{
    public class ArticleGetListOutputDto : IEntityDto<long>
    {
        public long Id { get; set; }
        //������ѯ���������ݣ����ܿ���
        //public string Content { get; set; }
        public string Name { get; set; }
        public long DiscussId { get; set; }

        public List<ArticleGetListOutputDto> Children { get; set; }
    }
}