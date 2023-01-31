using Yi.RBAC.Application.Contracts.Identity;
using NET.AutoWebApi.Setting;
using Yi.RBAC.Application.Contracts.Identity.Dtos;
using Yi.RBAC.Domain.Identity.Entities;
using Yi.Framework.Ddd.Services;
using Yi.RBAC.Domain.Shared.Identity.ConstClasses;
using Yi.RBAC.Domain.Identity;
using Yi.Framework.Uow;

namespace Yi.RBAC.Application.Identity
{
    /// <summary>
    /// User服务实现
    /// </summary>
    [AppService]
    public class UserService : CrudAppService<UserEntity, UserGetOutputDto, UserGetListOutputDto, long, UserGetListInputVo, UserCreateInputVo, UserUpdateInputVo>,
       IUserService, IAutoApiService
    {
        [Autowired]
        private UserManager _userManager { get; set; }

        [Autowired]
        private IUnitOfWorkManager _unitOfWorkManager { get; set; }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async override Task<UserGetOutputDto> CreateAsync(UserCreateInputVo input)
        {
            if (string.IsNullOrEmpty(input.Password))
            {
                throw new UserFriendlyException(UserConst.添加失败_密码为空);
            }
            if (await _repository.IsAnyAsync(u => input.UserName.Equals(u.UserName)))
            {
                throw new UserFriendlyException(UserConst.添加失败_用户存在);
            }
            var entities = await MapToEntityAsync(input);

            entities.BuildPassword();

            using (var uow = _unitOfWorkManager.CreateContext())
            {
                var returnEntity = await _repository.InsertReturnEntityAsync(entities);
                await _userManager.GiveUserSetRoleAsync(new List<long> { returnEntity.Id }, input.RoleIds);
                await _userManager.GiveUserSetPostAsync(new List<long> { returnEntity.Id }, input.PostIds);
                uow.Commit();

                var result = await MapToGetOutputDtoAsync(returnEntity);
                return result;
            }
        }
    }
}
