using Yi.RBAC.Application.Contracts.Identity;
using NET.AutoWebApi.Setting;
using Yi.RBAC.Application.Contracts.Identity.Dtos;
using Yi.RBAC.Domain.Identity.Entities;
using Yi.Framework.Ddd.Services;
using Yi.RBAC.Domain.Shared.Identity.ConstClasses;
using Yi.RBAC.Domain.Identity;
using Yi.Framework.Uow;
using Yi.Framework.Ddd.Dtos;
using Yi.RBAC.Domain.Identity.Repositories;
using SqlSugar;
using Mapster;

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

        [Autowired]
        private IUserRepository _userRepository { get; set; }

        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<PagedResultDto<UserGetListOutputDto>> GetListAsync(UserGetListInputVo input)
        {
            var entity = await MapToEntityAsync(input);

            RefAsync<int> total = 0;

            var entities = await _DbQueryable.WhereIF(!string.IsNullOrEmpty(input.UserName), x => x.UserName.Contains(input.UserName!)).
                          WhereIF(input.Phone is not null, x => x.Phone.ToString()!.Contains(input.Phone.ToString()!)).
                          WhereIF(!string.IsNullOrEmpty(input.Name), x => x.Name!.Contains(input.Name!)).
                          WhereIF(input.StartTime is not null && input.EndTime is not null, x => x.CreationTime >= input.StartTime && x.CreationTime <= input.EndTime).ToPageListAsync(input.PageNum, input.PageSize, total);

            var result = new PagedResultDto<UserGetListOutputDto>();
            result.Items = await MapToGetListOutputDtosAsync(entities);
            result.Total = total;
            return result;
        }

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
        /// <summary>
        /// 单查
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<UserGetOutputDto> GetAsync(long id)
        {
            //使用导航树形查询
            var entity = await _DbQueryable.Includes(u => u.Roles).Includes(u => u.Posts).Includes(u => u.Dept).InSingleAsync(id);

            return await MapToGetOutputDtoAsync(entity);
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async override Task<UserGetOutputDto> UpdateAsync(long id, UserUpdateInputVo input)
        {
            if (await _repository.IsAnyAsync(u => input.UserName!.Equals(u.UserName) && !id.Equals(u.Id)))
            {
                throw new UserFriendlyException("用户已经在，更新失败");
            }
            var entity = await _repository.GetByIdAsync(id);
            //更新密码，特殊处理
            if (input.Password is not null)
            {
                entity.Password = input.Password;
                entity.BuildPassword();
            }
            await MapToEntityAsync(input, entity);
            using (var uow = _unitOfWorkManager.CreateContext())
            {
                var res1 = await _repository.UpdateAsync(entity);
                await _userManager.GiveUserSetRoleAsync(new List<long> { id }, input.RoleIds);
                await _userManager.GiveUserSetPostAsync(new List<long> { id }, input.PostIds);
                uow.Commit();
            }
            return await MapToGetOutputDtoAsync(entity);
        }
    }
}
