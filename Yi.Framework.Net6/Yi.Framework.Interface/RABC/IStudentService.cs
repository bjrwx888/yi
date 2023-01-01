using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yi.Framework.DTOModel.RABC.Student;
using Yi.Framework.Interface.Base.Crud;

namespace Yi.Framework.Interface.RABC
{ 
    public interface IStudentService : ICrudAppService<StudentGetOutput, StudentListOutput, Guid, StudentCreateInput, StudentUpdateInput>
    {
        void GetError();
        Task<List<StudentListOutput>> GetListAsync();
    }
}