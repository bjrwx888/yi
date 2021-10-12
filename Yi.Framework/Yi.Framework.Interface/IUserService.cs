﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Model.Models;

namespace Yi.Framework.Interface
{
   public interface IUserService:IBaseService<user> 
    {
        Task<bool> DelListByUpdateAsync(List<int> _ids);
        Task<IEnumerable<user>> GetAllEntitiesTrueAsync();        
        Task<IEnumerable<user>> GetEntitiesTrueByIdAsync(List<int> _ids);       
        Task<bool> AddEntitesAsync(List<user> _users);      
        Task<bool> UpdateEntitesAsync(List<user> _users);

    }
}
