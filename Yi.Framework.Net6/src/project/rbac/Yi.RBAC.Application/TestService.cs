using Cike.AutoWebApi.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Services;

namespace Yi.RBAC.Application
{
    class Test01
    {

        public string Name { get; set; }
        public bool State { get; set; }
    }

    class Test02
    {
        public string Name { get; set; }
    }

    public class TestService : ApplicationService, IAutoApiService
    {

        public void Test()
        {
            var t001 = new Test02 { Name = "121233" };
            var t002 = new Test01 { Name = "123", State = true };

           var entity= _mapper.Map(t001, t002);
        }
    }
}
