using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Model;

namespace Yi.Framework.Ddd.Services
{
    public abstract class ApplicationService
    { 
        public IMapper _mapper { get => ServiceLocatorModel.Instance.GetRequiredService<IMapper>(); }
    }
}
