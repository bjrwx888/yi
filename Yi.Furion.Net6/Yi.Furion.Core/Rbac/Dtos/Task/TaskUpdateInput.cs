using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Furion.Core.Rbac.Enums;

namespace Yi.Furion.Core.Rbac.Dtos.Task
{
    public class TaskUpdateInput
    {
        public string AssemblyName { get; set; }

        public string JobTypeFullName { get; set; }

        public string GroupName { get; set; }

        public JobTypeEnum Type { get; set; }

        public string Cron { get; set; }

        public int Millisecond { get; set; }

        public bool Concurrent { get; set; }

        public Dictionary<string, object> Properties { get; set; }

        public string Description { get; set; }
    }
}
