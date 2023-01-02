using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Yi.Framework.Model.Base;

namespace Yi.Framework.DtoModel.RABC.Student
{
    public class StudentGetOutput : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string Remark { get; set; }

    }
}
