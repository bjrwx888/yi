﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Sqlsugar.Repositories;
using Yi.Framework.Domain.Student.Entities;
using Yi.Framework.Domain.Student.Repositories;

namespace Yi.Framework.Sqlsugar.Student
{
    /// <summary>
    /// 仓储实现方式
    /// </summary>
    public class StudentRepository : SqlsugarRepository<StudentEntity>, IStudentRepository
    {
        public StudentRepository(ISqlSugarClient context) : base(context)
        {
        }

        public async Task<List<StudentEntity>> GetMyListAsync()
        {
          return await _DbQueryable.ToListAsync();
        }
    }
}
