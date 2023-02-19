﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.BBS.Domain.Forum.Entities;
using Yi.BBS.Domain.Shared.Forum.ConstClasses;
using Yi.Framework.Ddd.Repositories;

namespace Yi.BBS.Domain.Forum
{
    /// <summary>
    /// 论坛模块的领域服务
    /// </summary>
    [AppService]
    public class ForumManager
    {
        private readonly IRepository<DiscussEntity> _discussRepository;
        private readonly IRepository<PlateEntity> _plateEntityRepository;
        private readonly IRepository<CommentEntity> _commentRepository;
        public ForumManager(IRepository<DiscussEntity> discussRepository, IRepository<PlateEntity> plateEntityRepository, IRepository<CommentEntity> commentRepository)
        {
            _discussRepository = discussRepository;
            _plateEntityRepository = plateEntityRepository;
            _commentRepository = commentRepository;
        }

        //主题是不能直接创建的，需要由领域服务统一创建
        public async Task<DiscussEntity> CreateDiscussAsync(long plateId, string title, string types, string content, string? introduction = null)
        {
            var entity = new DiscussEntity(plateId);
            entity.Id = SnowflakeHelper.NextId;
            entity.Title = title;
            entity.Types = types;
            entity.Introduction = introduction;
            entity.Content = content;
            entity.CreateTime = DateTime.Now;
            entity.AgreeNum = 0;
            entity.SeeNum = 0;
            return await _discussRepository.InsertReturnEntityAsync(entity);
        }

        public async Task<CommentEntity> CreateCommentAsync(long discussId, long userId, string content)
        {
            var entity = new CommentEntity(discussId, userId);
            entity.Id = SnowflakeHelper.NextId;
            entity.Content = content;
            entity.CreateTime = DateTime.Now;
            return await _commentRepository.InsertReturnEntityAsync(entity);
        }
    }
}