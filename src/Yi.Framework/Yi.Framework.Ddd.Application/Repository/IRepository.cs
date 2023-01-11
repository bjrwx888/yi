﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Ddd.Repository
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(dynamic id);
    }
}
