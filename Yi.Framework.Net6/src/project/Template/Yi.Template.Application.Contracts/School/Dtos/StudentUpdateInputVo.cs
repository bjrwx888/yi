using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Template.Application.Contracts.School.Dtos
{
    public class StudentUpdateInputVo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int? Height { get; set; }
        public string? Phone { get; set; }
    }
}
