using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.BBS.Application.Contracts.Exhibition.Dtos.Argee
{
    public class ArgeeDto
    {
        public ArgeeDto(bool isArgee)
        {
            IsArgee = isArgee;
            if (isArgee)
            {
              
                Message = "点赞成功，点赞+1";
            }
            else
            {

                Message = "取消点赞，点赞-1";
            }
           
        }

        public bool IsArgee { get; set; }
        public string Message { get; set; }
    }
}
