using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Interface
{
    public interface IBaseService<T> where T: class, IBaseModelEntity,new()
    {
        public IRepository<T> _repository { get; set; }
    }
}
