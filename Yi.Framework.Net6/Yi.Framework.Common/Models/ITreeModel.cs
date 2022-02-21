using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Common.Models
{
    public interface ITreeModel<T>
    {
        public int id { get; set; }
        public int parentId { get; set; }
        public int sort { get; set; }

        public IList<T> children { get; set; }
    }
}
