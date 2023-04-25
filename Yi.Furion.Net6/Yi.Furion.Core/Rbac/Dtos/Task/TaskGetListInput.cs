using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Infrastructure.Ddd.Dtos;

namespace Yi.Furion.Core.Rbac.Dtos.Task
{
    public class TaskGetListInput: PagedAllResultRequestDto
    {
        public string JobId { get; set; }
        public string GroupName { get; set; }
    }
}
