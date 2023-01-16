using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Repositories;
using Yi.Framework.Domain.Student.Entities;

namespace Yi.Framework.Domain.Student.IRepository
{
    /// <summary>
    /// 仓储抽象
    /// </summary>
    public interface IStudentRepository:IRepository<StudentEntity>
    {
        Task<List<StudentEntity>> GetMyListAsync();
    }
}
