using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Dtos;

namespace Yi.Template.Application.Contracts.School.Dtos
{
    public class StudentGetListOutputDto : IEntityDto<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int? Height { get; set; }
        public string? Phone { get; set; }
    }
}
