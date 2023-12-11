﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus;
using Yi.Framework.Rbac.Domain.Entities;
using Yi.Framework.Rbac.Domain.Shared.Etos;

namespace Yi.Framework.Rbac.Application.Events
{
    public class LoginEventHandler : ILocalEventHandler<LoginEventArgs>,
          ITransientDependency
    {
        private readonly ILogger<LoginEventHandler> _logger;
        private readonly IRepository<LoginLogEntity> _loginLogRepository;
        public LoginEventHandler(ILogger<LoginEventHandler> logger, IRepository<LoginLogEntity> loginLogRepository) { _logger = logger; _loginLogRepository = loginLogRepository; }
        public Task HandleEventAsync(LoginEventArgs eventData)
        {
            _logger.LogInformation($"用户【{eventData.UserId}:{eventData.UserName}】登入系统");
            var loginLogEntity = eventData.Adapt<LoginLogEntity>();
            loginLogEntity.LogMsg = eventData.UserName + "登录系统";
            loginLogEntity.LoginUser = eventData.UserName;
            loginLogEntity.CreatorId = eventData.UserId;
            //异步插入
            _loginLogRepository.InsertAsync(loginLogEntity);
            return Task.CompletedTask;
        }
    }
}
