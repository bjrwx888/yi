using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.BBS.Application.Contracts.GlobalSetting.Dtos.Temp
{
    public class ActionJwtDto
    {
        public int Id { get; set; }
        public string ActionName { get; set; }
        public string Router { get; set; }
        public string Icon { get; set; }
    }
}
