﻿using Mapster;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Yi.Framework.Bbs.Application.Contracts.Dtos.Analyse;
using Yi.Framework.Bbs.Application.Contracts.Dtos.BbsUser;
using Yi.Framework.Bbs.Domain.Entities;
using Yi.Framework.Bbs.Domain.Entities.Integral;
using Yi.Framework.Bbs.Domain.Managers;
using Yi.Framework.Bbs.Domain.Shared.Enums;
using Yi.Framework.DigitalCollectibles.Application.Contracts.IServices;
using Yi.Framework.Rbac.Application.Contracts.IServices;
using Yi.Framework.Rbac.Domain.Authorization;
using Yi.Framework.Rbac.Domain.Shared.Consts;
using Yi.Framework.Rbac.Domain.Shared.Model;

namespace Yi.Framework.Bbs.Application.Services.Analyses
{
    public class BbsUserAnalyseService : ApplicationService, IApplicationService
    {
        private BbsUserManager _bbsUserManager;
        private IOnlineService _onlineService;
        private readonly IPointAnalyseService _pointAnalyseService;
        private readonly IValueAnalyseService _valueAnalyseService;

        public BbsUserAnalyseService(BbsUserManager bbsUserManager, IOnlineService onlineService,
            IPointAnalyseService pointAnalyseService, IValueAnalyseService valueAnalyseService)
        {
            _bbsUserManager = bbsUserManager;
            _onlineService = onlineService;
            _pointAnalyseService = pointAnalyseService;
            _valueAnalyseService = valueAnalyseService;
        }


        /// <summary>
        /// 人数注册统计(近3个月)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("analyse/bbs-user/register")]
        public async Task<List<RegisterAnalyseDto>> GetRegisterAsync()

        {
            using (DataFilter.DisablePermissionHandler())
            {
                var users = await _bbsUserManager._userRepository._DbQueryable
                    .Where(u => u.CreationTime >= DateTime.Now.AddMonths(-3))
                    .LeftJoin<BbsUserExtraInfoEntity>((u, info) => u.Id == info.UserId)
                    .Select((u, info) => new BbsUserGetListOutputDto()
                    {
                        Id = u.Id,
                        Icon = u.Icon,
                        Level = info.Level,
                        UserLimit = info.UserLimit,
                        Money = info.Money,
                        Experience = info.Experience,
                        CreationTime = u.CreationTime
                    })
                    .ToListAsync();

                var minCreateUser = users.MinBy(x => x.CreationTime);

                var userCreateTimeDic = users.OrderBy(x => x.CreationTime)
                    .GroupBy(x => x.CreationTime.Date)
                    .ToDictionary(x => x.Key.Date, y => y.Count());

                DateTime startDate = minCreateUser.CreationTime.Date;
                DateTime endDate = DateTime.Today;

                List<RegisterAnalyseDto> output = new List<RegisterAnalyseDto>();

                // 计算从起始日期到今天的所有天数
                for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    var count = 0;
                    userCreateTimeDic.TryGetValue(date, out count);
                    RegisterAnalyseDto dayInfo = new RegisterAnalyseDto(date, count);
                    output.Add(dayInfo);
                }

                return output;
            }
        }


        /// <summary>
        /// 财富排行榜
        /// </summary>
        /// <returns></returns>
        [HttpGet("analyse/bbs-user/money-top/{userId?}")]
        public async Task<PagedResultDto<MoneyTopUserDto>> GetMoneyTopAsync([FromQuery] PagedResultRequestDto input,
            [FromRoute] Guid? userId)
        {
            using (DataFilter.DisablePermissionHandler())
            {
                var pageIndex = input.SkipCount;


                RefAsync<int> total = 0;
                var output = await _bbsUserManager._userRepository._DbQueryable
                    .LeftJoin<BbsUserExtraInfoEntity>((u, info) => u.Id == info.UserId)
                    .OrderByDescending((u, info) => info.Money)
                    .Select((u, info) =>
                        new MoneyTopUserDto
                        {
                            UserName = u.UserName,
                            Nick = u.Nick,
                            Money = info.Money,
                            Icon = u.Icon,
                            Level = info.Level,
                            UserLimit = info.UserLimit,
                            Order = SqlFunc.RowNumber(SqlFunc.Desc(info.Money))
                        }
                    )
                    .ToPageListAsync(pageIndex, input.MaxResultCount, total);

                output.ForEach(x => { x.LevelName = _bbsUserManager._levelCacheDic[x.Level].Name; });
                return new PagedResultDto<MoneyTopUserDto>
                {
                    Items = output,
                    TotalCount = total
                };
            }
        }


        /// <summary>
        /// 推荐好友，随机返回好友列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("analyse/bbs-user/random")]
        public async Task<List<BbsUserGetListOutputDto>> GetRandomUserAsync([FromQuery] PagedResultRequestDto input)
        {
            using (DataFilter.DisablePermissionHandler())
            {
                var randUserIds = await _bbsUserManager._userRepository._DbQueryable
                    //.Where(x => x.UserName != UserConst.Admin)
                    .OrderBy(x => SqlFunc.GetRandom())
                    .Select(x => x.Id).ToPageListAsync(input.SkipCount, input.MaxResultCount);
                var output = await _bbsUserManager.GetBbsUserInfoAsync(randUserIds);
                return output.Adapt<List<BbsUserGetListOutputDto>>();

                //这里关闭了数据权限，所有用户都能查询的到
            }
            //这里有数据权限，会根据用户角色进行过滤
        }

        /// <summary>
        /// 用户分析
        /// </summary>
        /// <returns></returns>
        [HttpGet("analyse/bbs-user")]
        public async Task<BbsUserAnalyseGetOutput> GetUserAnalyseAsync()
        {
            using (DataFilter.DisablePermissionHandler())
            {
                var sss = DataFilter.IsEnabled<IDataPermission>();
                var registerUser = await _bbsUserManager._userRepository._DbQueryable.CountAsync();


                DateTime now = DateTime.Now;
                DateTime yesterday = now.AddDays(-1);
                DateTime startTime = new DateTime(yesterday.Year, yesterday.Month, yesterday.Day, 0, 0, 0);
                DateTime endTime = startTime.AddHours(24);
                var yesterdayNewUser = await _bbsUserManager._userRepository._DbQueryable
                    .Where(x => x.CreationTime >= startTime && x.CreationTime <= endTime).CountAsync();

                var userOnline = (await _onlineService.GetListAsync(new OnlineUserModel { })).TotalCount;

                var output = new BbsUserAnalyseGetOutput()
                    { OnlineNumber = userOnline, RegisterNumber = registerUser, YesterdayNewUser = yesterdayNewUser };

                return output;
            }
        }


        /// <summary>
        /// 积分排行榜
        /// </summary>
        /// <returns></returns>
        [HttpGet("analyse/dc-user/points-top/{userId?}")]
        public async Task<PagedResultDto<PointsTopUserDto>> GetPointsTopAsync([FromQuery] PagedResultRequestDto input,
            [FromRoute] Guid? userId)
        {
            var result = await _pointAnalyseService.GetValueTopAsync(input, null);

            RefAsync<int> total = 0;
            var userIds = result.Items.Select(x => x.UserId).ToList();

            var baseOutput = await _bbsUserManager._userRepository._DbQueryable
                .Where(u => userIds.Contains(u.Id))
                .LeftJoin<BbsUserExtraInfoEntity>((u, info) => u.Id == info.UserId)
                .OrderByDescending((u, info) => info.Money)
                .Select((u, info) =>
                    new BaseAnalyseTopUserDto
                    {
                        UserName = u.UserName,
                        Nick = u.Nick,
                        Icon = u.Icon,
                        Level = info.Level,
                        UserLimit = info.UserLimit,
                        UserId = info.UserId
                    }
                ).ToPageListAsync(input.SkipCount, input.MaxResultCount, total);


            var output = new List<PointsTopUserDto>();
            result.Items.ToList().ForEach(x =>
            {
                var currentUserInfo = baseOutput.Where(u => u.UserId == x.UserId).FirstOrDefault();

                if (currentUserInfo is not null)
                {
                    output.Add(new PointsTopUserDto
                    {
                        UserName = currentUserInfo.UserName,
                        Nick = currentUserInfo.Nick,
                        Order = x.Order,
                        Icon = currentUserInfo.Icon,
                        Level = currentUserInfo.Level,
                        LevelName = _bbsUserManager._levelCacheDic[currentUserInfo.Level].Name,
                        UserLimit = UserLimitEnum.Normal,
                        Points = x.Points
                    });
                }
            });
            return new PagedResultDto<PointsTopUserDto>
            {
                Items = output,
                TotalCount = total
            };
        }

        /// <summary>
        /// 价值排行榜
        /// </summary>
        /// <returns></returns>
        [HttpGet("analyse/dc-user/value-top/{userId?}")]
        public async Task<PagedResultDto<ValueTopUserDto>> GetValueTopAsync([FromQuery] PagedResultRequestDto input,
            [FromRoute] Guid? userId)
        {
            var result = await _valueAnalyseService.GetValueTopAsync(input, null);

            RefAsync<int> total = 0;
            var userIds = result.Items.Select(x => x.UserId).ToList();

            var baseOutput = await _bbsUserManager._userRepository._DbQueryable
                .Where(u => userIds.Contains(u.Id))
                .LeftJoin<BbsUserExtraInfoEntity>((u, info) => u.Id == info.UserId)
                .OrderByDescending((u, info) => info.Money)
                .Select((u, info) =>
                    new BaseAnalyseTopUserDto
                    {
                        UserName = u.UserName,
                        Nick = u.Nick,
                        Icon = u.Icon,
                        Level = info.Level,
                        UserLimit = info.UserLimit,
                        UserId = info.UserId
                    }
                ).ToPageListAsync(input.SkipCount, input.MaxResultCount, total);


            var output = new List<ValueTopUserDto>();
            result.Items.ToList().ForEach(x =>
            {
                var currentUserInfo = baseOutput.Where(u => u.UserId == x.UserId).FirstOrDefault();

                if (currentUserInfo is not null)
                {
                    output.Add(new ValueTopUserDto
                    {
                        UserName = currentUserInfo.UserName,
                        Nick = currentUserInfo.Nick,
                        Order = x.Order,
                        Icon = currentUserInfo.Icon,
                        Level = currentUserInfo.Level,
                        LevelName = _bbsUserManager._levelCacheDic[currentUserInfo.Level].Name,
                        UserLimit = UserLimitEnum.Normal,
                        Value = x.Value
                    });
                }
            });
            return new PagedResultDto<ValueTopUserDto>
            {
                Items = output,
                TotalCount = total
            };
        }
    }
}