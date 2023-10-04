using System;
using System.Collections.Generic;
using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;
using Yi.Furion.Core.Bbs.Consts;
using Yi.Furion.Core.Bbs.Enums;
using Yi.Furion.Core.Rbac.Dtos.User;

namespace Yi.Furion.Core.Bbs.Dtos.Discuss
{
    public class DiscussGetListOutputDto : IEntityDto<long>
    {
        /// <summary>
        /// 是否已点赞
        /// </summary>
        public bool IsAgree { get; set; }
        public long Id { get; set; }
        public string Title { get; set; }
        public string Types { get; set; }
        public string? Introduction { get; set; }

        public int AgreeNum { get; set; }
        public int SeeNum { get; set; }

        //批量查询，不给内容，性能考虑
        //public string Content { get; set; }
        public string? Color { get; set; }

        public long PlateId { get; set; }

        //是否置顶，默认false
        public bool IsTop { get; set; }

        public DiscussPermissionTypeEnum PermissionType { get; set; }
        //是否禁止，默认false
        public bool IsBan { get; set; }


        /// <summary>
        /// 封面
        /// </summary>
        public string? Cover { get; set; }

        //私有需要判断code权限
        public string? PrivateCode { get; set; }
        public DateTime CreationTime { get; set; }

        public List<long> PermissionUserIds { get; set; }

        public UserGetListOutputDto User { get; set; }

        public void SetBan()
        {
            this.Title = DiscussConst.Privacy;
            this.Introduction = "";
            this.Cover = null;
            //被禁止
            this.IsBan = true;
        }
    }


    public static class DiscussGetListOutputDtoExtension
    {

        public static void ApplyPermissionTypeFilter(this List<DiscussGetListOutputDto> dtos, long userId)
        {
            dtos?.ForEach(dto =>
            {
                switch (dto.PermissionType)
                {
                    case DiscussPermissionTypeEnum.Public:
                        break;
                    case DiscussPermissionTypeEnum.Oneself:
                        //当前主题是仅自己可见，同时不是当前登录用户
                        if (dto.User.Id != userId)
                        {
                            dto.SetBan();
                        }
                        break;
                    case DiscussPermissionTypeEnum.User:
                        //当前主题为部分可见，同时不是当前登录用户 也 不在可见用户列表中
                        if (dto.User.Id != userId && !dto.PermissionUserIds.Contains(userId))
                        {
                            dto.SetBan();
                        }
                        break;
                    default:
                        break;
                }
            });
        }

    }

}
