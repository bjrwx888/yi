using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Application.Contracts.Student;
using Yi.Framework.Domain.Student;
using Yi.Framework.Domain.Student.IRepository;
using Microsoft.AspNetCore.Mvc;
using NET.AutoWebApi.Setting;
using Microsoft.AspNetCore.Http;
using Yi.Framework.Ddd.Services.Abstract;
using Yi.Framework.Application.Contracts.Student.Dtos;
using Yi.Framework.Domain.Student.Entities;
using Yi.Framework.Ddd.Services;
using Yi.Framework.Core.Attributes;
using Yi.Framework.Uow;
using Microsoft.AspNetCore.Authorization;
using Yi.Framework.Auth.JwtBearer.Authentication;
using Yi.Framework.Core.Const;
using Yi.Framework.Core.CurrentUsers;
using Yi.Framework.Auth.JwtBearer.Authorization;
using Yi.Framework.Domain.Shared.Student.ConstClasses;

namespace Yi.Framework.Application.Student
{
    /// <summary>
    /// 服务实现
    /// </summary>

    [AppService]
    public class StudentService : CrudAppService<StudentEntity, StudentGetOutputDto, StudentGetListOutputDto, long, StudentGetListInputVo, StudentCreateInputVo, StudentUpdateInputVo>,
       IStudentService, IAutoApiService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly StudentManager _studentManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly JwtTokenManager _jwtTokenManager;
        private readonly ICurrentUser _currentUser;
        public StudentService(IStudentRepository studentRepository, StudentManager studentManager, IUnitOfWorkManager unitOfWorkManager, JwtTokenManager jwtTokenManager, ICurrentUser currentUser)
        {
            _studentRepository = studentRepository;
            _studentManager = studentManager;
            _unitOfWorkManager = unitOfWorkManager;
            _jwtTokenManager = jwtTokenManager;
            _currentUser=currentUser;
        }

        /// <summary>
        /// 测试token
        /// </summary>
        /// <returns></returns>
        public  string GetToken()
        {
            var claimDic = new Dictionary<string, object>() { { TokenTypeConst.Id, "123" }, { TokenTypeConst.UserName, "cc" } };
            return _jwtTokenManager.CreateToken(claimDic);
        }

        /// <summary>
        /// Uow
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Permission(AuthStudentConst.查询)]
        public async Task<StudentGetOutputDto> PostUow()
        {
        var o=    _currentUser;
            StudentGetOutputDto res = new();
            using (var uow = _unitOfWorkManager.CreateContext())
            {
                var studentRepository = uow.GetRepository<StudentEntity>();
                res = await base.CreateAsync(new StudentCreateInputVo { Name = $"老杰哥{DateTime.Now.ToString("ffff")}", Number = 2023 });
                if (new Random().Next(0, 2) == 0) throw new NotImplementedException();
                uow.Commit();
            }
            return res;
        }
    }
}
