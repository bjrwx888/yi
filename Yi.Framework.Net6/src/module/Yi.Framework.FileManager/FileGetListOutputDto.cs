using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Dtos;

namespace Yi.Framework.FileManager
{
    public class FileGetListOutputDto:IEntityDto
    {
        public long Id { get; set; }
    }
}
