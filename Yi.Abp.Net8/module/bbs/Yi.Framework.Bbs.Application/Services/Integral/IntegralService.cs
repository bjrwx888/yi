using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Yi.Framework.Bbs.Domain.Managers;

namespace Yi.Framework.Bbs.Application.Services.Integral
{
    public class IntegralService:ApplicationService
    {
        private IntegralManager _integralManager;
        public IntegralService(IntegralManager integralManager)
        {
            _integralManager= integralManager;
        }

    }
}
