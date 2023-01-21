using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Dtos;

namespace Yi.Framework.Application.Contracts.Student.Dtos
{
    public class StudentGetOutputDto : IEntityDto<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public long Number { get; set; }
    }
}
