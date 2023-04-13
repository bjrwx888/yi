using SqlSugar;
using Yi.Framework.Infrastructure.CurrentUsers;
using Yi.Framework.Infrastructure.Ddd.Dtos;
using Yi.Framework.Infrastructure.Ddd.Services;
using Yi.Framework.Infrastructure.Exceptions;
using Yi.Framework.Infrastructure.Uow;
using Yi.Framework.Module.OperLogManager;
using Yi.Furion.Rbac.Application.System.Domain;
using Yi.Furion.Rbac.Application.System.Dtos.User;
using Yi.Furion.Rbac.Core.ConstClasses;
using Yi.Furion.Rbac.Core.Entities;
using Yi.Furion.Rbac.Sqlsugar.Core.Repositories;

namespace Yi.Furion.Rbac.Application.System.Services.Impl
{
    /// <summary>
    /// User服务实现
    /// </summary>
    public class UserService : CrudAppService<UserEntity, UserGetOutputDto, UserGetListOutputDto, long, UserGetListInputVo, UserCreateInputVo, UserUpdateInputVo>,
       IUserService, ITransient, IDynamicApiController
    {


        public UserService(UserManager userManager, IUserRepository userRepository, ICurrentUser currentUser, IUnitOfWorkManager unitOfWorkManager) =>
            (_userManager, _userRepository, _currentUser, _unitOfWorkManager) = 
            (userManager, userRepository, currentUser, unitOfWorkManager);
        private UserManager _userManager { get; set; }

        
        private IUnitOfWorkManager _unitOfWorkManager { get; set; }

        
        private IUserRepository _userRepository { get; set; }

        
        private ICurrentUser _currentUser { get; set; }

        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<PagedResultDto<UserGetListOutputDto>> GetListAsync(UserGetListInputVo input)
        {
            var entity = await MapToEntityAsync(input);

            RefAsync<int> total = 0;


            List<long> ids = input.Ids?.Split(",").Select(x => long.Parse(x)).ToList();
            var outPut = await _DbQueryable.WhereIF(!string.IsNullOrEmpty(input.UserName), x => x.UserName.Contains(input.UserName!))
                         .WhereIF(input.Phone is not null, x => x.Phone.ToString()!.Contains(input.Phone.ToString()!))
                          .WhereIF(!string.IsNullOrEmpty(input.Name), x => x.Name!.Contains(input.Name!))
                          .WhereIF(input.State is not null, x => x.State == input.State)
                          .WhereIF(input.StartTime is not null && input.EndTime is not null, x => x.CreationTime >= input.StartTime && x.CreationTime <= input.EndTime)

                          //这个为过滤当前部门，加入数据权限后，将由数据权限控制
                          .WhereIF(input.DeptId is not null, x => x.DeptId == input.DeptId)

                          .WhereIF(ids is not null, x => ids.Contains(x.Id))


                          .LeftJoin<DeptEntity>((user, dept) => user.DeptId == dept.Id)
                          .Select((user, dept) => new UserGetListOutputDto(), true)
                          .ToPageListAsync(input.PageNum, input.PageSize, total);

            var result = new PagedResultDto<UserGetListOutputDto>();
            result.Items = outPut;
            result.Total = total;
            return result;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        [OperLog("添加用户", OperEnum.Insert)]
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
        [OperLog("更新用户", OperEnum.Update)]
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

        /// <summary>
        /// 更新个人中心
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [OperLog("更新个人信息", OperEnum.Update)]
        public async Task<UserGetOutputDto> UpdateProfileAsync(ProfileUpdateInputVo input)
        {
            var entity = await _repository.GetByIdAsync(_currentUser.Id);
            _mapper.Map(input, entity);
            await _repository.UpdateAsync(entity);
            var dto = _mapper.Map<UserGetOutputDto>(entity);
            return dto;
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [Route("/api/user/{id}/{state}")]
        [OperLog("更新用户状态", OperEnum.Update)]
        public async Task<UserGetOutputDto> UpdateStateAsync([FromRoute] long id, [FromRoute] bool state)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null)
            {
                throw new ApplicationException("用户未存在");
            }

            entity.State = state;
            await _repository.UpdateAsync(entity);
            return await MapToGetOutputDtoAsync(entity);
        }
        [OperLog("删除用户", OperEnum.Delete)]
        public override Task<bool> DeleteAsync(string id)
        {
            return base.DeleteAsync(id);
        }
    }
}
