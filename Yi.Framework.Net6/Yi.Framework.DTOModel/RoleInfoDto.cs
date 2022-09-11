using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Model.Models;

namespace Yi.Framework.DTOModel
{
    public class RoleInfoDto
    {
        public RoleEntity Role { get; set; }
        public List<long> DeptIds { get; set; }
        public List<long> MenuIds { get; set; }
    }
}
