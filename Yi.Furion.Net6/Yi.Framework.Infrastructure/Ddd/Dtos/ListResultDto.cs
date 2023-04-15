using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;

namespace Yi.Framework.Infrastructure.Ddd.Dtos
{
    [Serializable]
    public class ListResultDto<T> : IListResult<T>
    {
        public IReadOnlyList<T> Items
        {
            get { return _items ?? (_items = new List<T>()); }
            set { _items = value; }
        }
        private IReadOnlyList<T> _items;

        public ListResultDto()
        {

        }

        public ListResultDto(IReadOnlyList<T> items)
        {
            Items = items;
        }
    }
}
