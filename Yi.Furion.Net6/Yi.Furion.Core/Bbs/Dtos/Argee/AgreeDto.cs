using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Furion.Core.Bbs.Dtos.Argee
{
    public class AgreeDto
    {
        public AgreeDto(bool isAgree)
        {
            IsAgree = isAgree;
            if (isAgree)
            {

                Message = "点赞成功，点赞+1";
            }
            else
            {

                Message = "取消点赞，点赞-1";
            }

        }

        public bool IsAgree { get; set; }
        public string Message { get; set; }
    }
}
