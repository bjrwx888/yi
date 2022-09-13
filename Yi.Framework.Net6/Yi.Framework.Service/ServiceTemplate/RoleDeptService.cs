﻿using SqlSugar;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class RoleDeptService : BaseService<RoleDeptEntity>, IRoleDeptService
    {
        public RoleDeptService(IRepository<RoleDeptEntity> repository) : base(repository)
        {
        }
    }
}
