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
        public string Name { get; set; }
        public long Number { get; set; }

        /// <summary>
        /// 想看一下结果
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
