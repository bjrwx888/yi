using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Common.Models
{
    public interface ITreeModel<T>
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int Sort { get; set; }

        public IList<T> Children { get; set; }
    }
}
