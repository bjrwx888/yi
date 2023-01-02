using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yi.Framework.Common.Exceptions;
using Yi.Framework.DtoModel.RABC.Student;
using Yi.Framework.DtoModel.RABC.Student.ConstConfig;
using Yi.Framework.Interface.RABC;
using Yi.Framework.Model.RABC.Entitys;
using Yi.Framework.Repository;
using Yi.Framework.Service.Base.Crud;

namespace Yi.Framework.Service.RABC
{
    public class StudentService : CrudAppService<StudentEntity, StudentGetOutput, StudentListOutput, Guid, StudentCreateInput, StudentUpdateInput>, IStudentService
    {
        public StudentService(IRepository<StudentEntity> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<List<StudentListOutput>> GetListAsync()
        {
            return await MapToGetListOutputDtosAsync(await Repository.GetListAsync());
        }

        public  void GetError()
        {
            throw new ApplicationException(StudentConst.学生异常错误);
        }
        public void GetError2()
        {
            throw new UserFriendlyException(StudentConst.学生友好错误);
        }

        /// <summary>
        /// 校验配置项是否已存在
        /// </summary>
        /// <param name="key"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        //private async Task ValidateKeyAsync(string key, Guid? id = null)
        //{
        //    Expression<Func<StudentEntity, bool>> expression = e => e.Name == key;
        //    if (id.HasValue)
        //    {
        //        expression = expression.And(e => e.Id != id.Value);
        //    }
        //    var existsData = await Repository.GetListAsync(expression);
        //    if (existsData != null)
        //    {
        //        throw new UserFriendlyException();
        //    }
        //}
    }
}