using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Ddd.Services
{
    public abstract class ApplicationService
    { 
        public IMapper _mapper { get; set; }
    }
}
