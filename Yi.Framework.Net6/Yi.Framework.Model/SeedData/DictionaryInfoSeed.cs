﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Model.Models;

namespace Yi.Framework.Model.SeedData
{
    public class DictionaryInfoSeed : AbstractSeed<DictionaryInfoEntity>
    {
        public override List<DictionaryInfoEntity> GetSeed()
        {
            DictionaryInfoEntity dictInfo1 = new DictionaryInfoEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "男",
                DictValue= "0",
                DictType = "sys_user_sex",
                OrderNum = 100,
                Remark = "性别男",
                IsDeleted = false,
            };
            Entitys.Add(dictInfo1);

            DictionaryInfoEntity dictInfo2 = new DictionaryInfoEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "女",
                DictValue = "1",
                DictType = "sys_user_sex",
                OrderNum = 99,
                Remark = "性别女",
                IsDeleted = false,
            };
            Entitys.Add(dictInfo2);

            DictionaryInfoEntity dictInfo3 = new DictionaryInfoEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "未知",
                DictValue = "2",
                DictType = "sys_user_sex",
                OrderNum = 98,
                Remark = "性别未知",
                IsDeleted = false,
            };
            Entitys.Add(dictInfo3);



            DictionaryInfoEntity dictInfo4 = new DictionaryInfoEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "显示",
                DictValue = "0",
                DictType = "sys_show_hide",
                OrderNum = 100,
                Remark = "显示菜单",
                IsDeleted = false,
            };
            Entitys.Add(dictInfo4);

            DictionaryInfoEntity dictInfo5 = new DictionaryInfoEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "隐藏",
                DictValue = "1",
                DictType = "sys_show_hide",
                OrderNum = 99,
                Remark = "隐藏菜单",
                IsDeleted = false,
            };
            Entitys.Add(dictInfo5);



            DictionaryInfoEntity dictInfo6= new DictionaryInfoEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "正常",
                DictValue = "false",
                DictType = "sys_normal_disable",
                OrderNum = 100,
                Remark = "正常状态",
                IsDeleted = false,
            };
            Entitys.Add(dictInfo6);
            DictionaryInfoEntity dictInfo7 = new DictionaryInfoEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "停用",
                DictValue = "true",
                DictType = "sys_normal_disable",
                OrderNum = 99,
                Remark = "停用状态",
                IsDeleted = false,
                ListClass= "danger"
            };
            Entitys.Add(dictInfo7);



            DictionaryInfoEntity dictInfo8 = new DictionaryInfoEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "正常",
                DictValue = "0",
                DictType = "sys_job_status",
                OrderNum = 100,
                Remark = "正常状态",
                IsDeleted = false,
            };
            Entitys.Add(dictInfo8);
            DictionaryInfoEntity dictInfo9 = new DictionaryInfoEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "暂停",
                DictValue = "1",
                DictType = "sys_job_status",
                OrderNum = 99,
                Remark = "停用状态",
                IsDeleted = false,
                ListClass = "danger"
            };
            Entitys.Add(dictInfo9);




            DictionaryInfoEntity dictInfo10 = new DictionaryInfoEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "默认",
                DictValue = "DEFAULT",
                DictType = "sys_job_group",
                OrderNum = 100,
                Remark = "默认分组",
                IsDeleted = false,
            };
            Entitys.Add(dictInfo10);
            DictionaryInfoEntity dictInfo11 = new DictionaryInfoEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "系统",
                DictValue = "SYSTEM",
                DictType = "sys_job_group",
                OrderNum = 99,
                Remark = "系统分组",
                IsDeleted = false,
            };
            Entitys.Add(dictInfo11);



            DictionaryInfoEntity dictInfo12 = new DictionaryInfoEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "是",
                DictValue = "Y",
                DictType = "sys_yes_no",
                OrderNum = 100,
                Remark = "系统默认是",
                IsDeleted = false,
            };
            Entitys.Add(dictInfo12);
            DictionaryInfoEntity dictInfo13 = new DictionaryInfoEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "否",
                DictValue = "N",
                DictType = "sys_yes_no",
                OrderNum = 99,
                Remark = "系统默认否",
                IsDeleted = false,
                ListClass = "danger"
            };
            Entitys.Add(dictInfo13);



            DictionaryInfoEntity dictInfo14 = new DictionaryInfoEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "通知",
                DictValue = "1",
                DictType = "sys_notice_type",
                OrderNum = 100,
                Remark = "通知",
                IsDeleted = false,
            };
            Entitys.Add(dictInfo14);
            DictionaryInfoEntity dictInfo15 = new DictionaryInfoEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "公告",
                DictValue = "2",
                DictType = "sys_notice_type",
                OrderNum = 99,
                Remark = "公告",
                IsDeleted = false,
            };
            Entitys.Add(dictInfo15);

            DictionaryInfoEntity dictInfo16 = new DictionaryInfoEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "正常",
                DictValue = "0",
                DictType = "sys_notice_status",
                OrderNum = 100,
                Remark = "正常状态",
                IsDeleted = false,
            };
            Entitys.Add(dictInfo16);
            DictionaryInfoEntity dictInfo17 = new DictionaryInfoEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "关闭",
                DictValue = "1",
                DictType = "sys_notice_status",
                OrderNum = 99,
                Remark = "关闭状态",
                IsDeleted = false,
                ListClass = "danger"
            };
            Entitys.Add(dictInfo17);

            DictionaryInfoEntity dictInfo18 = new DictionaryInfoEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "正常",
                DictValue = "0",
                DictType = "sys_oper_type",
                OrderNum = 100,
                Remark = "正常状态",
                IsDeleted = false,
            };
            Entitys.Add(dictInfo18);
            DictionaryInfoEntity dictInfo19 = new DictionaryInfoEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "关闭",
                DictValue = "1",
                DictType = "sys_oper_type",
                OrderNum = 99,
                Remark = "关闭状态",
                IsDeleted = false,
                ListClass = "danger"
            };
            Entitys.Add(dictInfo19);




            DictionaryInfoEntity dictInfo20 = new DictionaryInfoEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "成功",
                DictValue = "0",
                DictType = "sys_common_status",
                OrderNum = 100,
                Remark = "正常状态",
                IsDeleted = false,
            };
            Entitys.Add(dictInfo20);
            DictionaryInfoEntity dictInfo21 = new DictionaryInfoEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "失败",
                DictValue = "1",
                DictType = "sys_common_status",
                OrderNum = 99,
                Remark = "失败状态",
                IsDeleted = false,
                ListClass = "danger"
            };
            Entitys.Add(dictInfo21);

            return Entitys;
        }
    }
}
