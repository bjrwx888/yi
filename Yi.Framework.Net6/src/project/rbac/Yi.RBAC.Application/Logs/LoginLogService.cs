using Microsoft.AspNetCore.Mvc;
using Cike.AutoWebApi.Setting;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Dtos;
using Yi.Framework.Ddd.Services;
using Yi.RBAC.Application.Contracts.Logs.Dtos.LoginLog;
using Yi.RBAC.Application.Contracts.Setting.Dtos;
using Yi.RBAC.Domain.Logs.Entities;

namespace Yi.RBAC.Application.Logs
{
    [AppService]
    public class LoginLogService : CrudAppService<LoginLogEntity, LoginLogGetListOutputDto, long, LoginLogGetListInputVo>, IAutoApiService
    {
        public override async Task<PagedResultDto<LoginLogGetListOutputDto>> GetListAsync(LoginLogGetListInputVo input)
        {
            var entity = await MapToEntityAsync(input);

            RefAsync<int> total = 0;

            var entities = await _DbQueryable.WhereIF(!string.IsNullOrEmpty(input.LoginIp), x => x.LoginIp.Contains(input.LoginIp!))
                          .WhereIF(!string.IsNullOrEmpty(input.LoginUser), x => x.LoginUser!.Contains(input.LoginUser!))
                          .WhereIF(input.StartTime is not null && input.EndTime is not null, x => x.CreationTime >= input.StartTime && x.CreationTime <= input.EndTime)
                          .ToPageListAsync(input.PageNum, input.PageSize, total);
            return new PagedResultDto<LoginLogGetListOutputDto>(total, await MapToGetListOutputDtosAsync(entities));
        }

        [NonAction]
        public override Task<LoginLogGetListOutputDto> UpdateAsync(long id, LoginLogGetListOutputDto input)
        {
            return base.UpdateAsync(id, input);
        }
    }
}
