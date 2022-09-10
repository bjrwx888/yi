using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public class BaseService<T>:IBaseService<T> where T:class,new()
    {
        public IRepository<T> _repository { get; set; }
        public BaseService(IRepository<T> iRepository)
        {
            _repository = iRepository;
        }
    }
}
