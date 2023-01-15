using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Dtos;

namespace Yi.Framework.Application.Contracts.Student.Dtos
{
    public class StudentGetListOutputDto : IEntityDto<long>
    {
        public long Id { get; set; }
    }
}
