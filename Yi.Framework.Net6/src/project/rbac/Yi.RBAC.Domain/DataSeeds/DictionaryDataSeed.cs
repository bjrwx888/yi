﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Data.DataSeeds;
using Yi.Framework.Ddd.Repositories;
using Yi.Framework.DictionaryManager.Entities;

namespace Yi.RBAC.Domain.DataSeeds
{
    [AppService(typeof(IDataSeed))]
    public class DictionaryDataSeed : AbstractDataSeed<DictionaryEntity>
    {
        public DictionaryDataSeed(IRepository<DictionaryEntity> repository) : base(repository)
        {
        }

        public override List<DictionaryEntity> GetSeedData()
        {
           List<DictionaryEntity> entities= new List<DictionaryEntity>();
            DictionaryEntity dictInfo1 = new DictionaryEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "男",
                DictValue = "0",
                DictType = "sys_user_sex",
                OrderNum = 100,
                Remark = "性别男",
                IsDeleted = false,
            };
            entities.Add(dictInfo1);

            DictionaryEntity dictInfo2 = new DictionaryEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "女",
                DictValue = "1",
                DictType = "sys_user_sex",
                OrderNum = 99,
                Remark = "性别女",
                IsDeleted = false,
            };
            entities.Add(dictInfo2);

            DictionaryEntity dictInfo3 = new DictionaryEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "未知",
                DictValue = "2",
                DictType = "sys_user_sex",
                OrderNum = 98,
                Remark = "性别未知",
                IsDeleted = false,
            };
            entities.Add(dictInfo3);



            DictionaryEntity dictInfo4 = new DictionaryEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "显示",
                DictValue = "true",
                DictType = "sys_show_hide",
                OrderNum = 100,
                Remark = "显示菜单",
                IsDeleted = false,
            };
            entities.Add(dictInfo4);

            DictionaryEntity dictInfo5 = new DictionaryEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "隐藏",
                DictValue = "false",
                DictType = "sys_show_hide",
                OrderNum = 99,
                Remark = "隐藏菜单",
                IsDeleted = false,
            };
            entities.Add(dictInfo5);



            DictionaryEntity dictInfo6 = new DictionaryEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "正常",
                DictValue = "true",
                DictType = "sys_normal_disable",
                OrderNum = 100,
                Remark = "正常状态",
                IsDeleted = false,
            };
            entities.Add(dictInfo6);
            DictionaryEntity dictInfo7 = new DictionaryEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "停用",
                DictValue = "false",
                DictType = "sys_normal_disable",
                OrderNum = 99,
                Remark = "停用状态",
                IsDeleted = false,
                ListClass = "danger"
            };
            entities.Add(dictInfo7);



            DictionaryEntity dictInfo8 = new DictionaryEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "正常",
                DictValue = "0",
                DictType = "sys_job_status",
                OrderNum = 100,
                Remark = "正常状态",
                IsDeleted = false,
            };
            entities.Add(dictInfo8);
            DictionaryEntity dictInfo9 = new DictionaryEntity()
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
            entities.Add(dictInfo9);




            DictionaryEntity dictInfo10 = new DictionaryEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "默认",
                DictValue = "DEFAULT",
                DictType = "sys_job_group",
                OrderNum = 100,
                Remark = "默认分组",
                IsDeleted = false,
            };
            entities.Add(dictInfo10);
            DictionaryEntity dictInfo11 = new DictionaryEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "系统",
                DictValue = "SYSTEM",
                DictType = "sys_job_group",
                OrderNum = 99,
                Remark = "系统分组",
                IsDeleted = false,
            };
            entities.Add(dictInfo11);



            DictionaryEntity dictInfo12 = new DictionaryEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "是",
                DictValue = "Y",
                DictType = "sys_yes_no",
                OrderNum = 100,
                Remark = "系统默认是",
                IsDeleted = false,
            };
            entities.Add(dictInfo12);
            DictionaryEntity dictInfo13 = new DictionaryEntity()
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
            entities.Add(dictInfo13);



            DictionaryEntity dictInfo14 = new DictionaryEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "通知",
                DictValue = "1",
                DictType = "sys_notice_type",
                OrderNum = 100,
                Remark = "通知",
                IsDeleted = false,
            };
            entities.Add(dictInfo14);
            DictionaryEntity dictInfo15 = new DictionaryEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "公告",
                DictValue = "2",
                DictType = "sys_notice_type",
                OrderNum = 99,
                Remark = "公告",
                IsDeleted = false,
            };
            entities.Add(dictInfo15);

            DictionaryEntity dictInfo16 = new DictionaryEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "正常",
                DictValue = "0",
                DictType = "sys_notice_status",
                OrderNum = 100,
                Remark = "正常状态",
                IsDeleted = false,
            };
            entities.Add(dictInfo16);
            DictionaryEntity dictInfo17 = new DictionaryEntity()
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
            entities.Add(dictInfo17);

            DictionaryEntity dictInfo18 = new DictionaryEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "新增",
                DictValue = "1",
                DictType = "sys_oper_type",
                OrderNum = 100,
                Remark = "新增操作",
                IsDeleted = false,
            };
            entities.Add(dictInfo18);
            DictionaryEntity dictInfo19 = new DictionaryEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "修改",
                DictValue = "2",
                DictType = "sys_oper_type",
                OrderNum = 99,
                Remark = "修改操作",
                IsDeleted = false,
            };
            entities.Add(dictInfo19);
            DictionaryEntity dictInfo22 = new DictionaryEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "删除",
                DictValue = "3",
                DictType = "sys_oper_type",
                OrderNum = 98,
                Remark = "删除操作",
                IsDeleted = false,
                ListClass = "danger"
            };
            entities.Add(dictInfo22);
            DictionaryEntity dictInfo23 = new DictionaryEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "授权",
                DictValue = "4",
                DictType = "sys_oper_type",
                OrderNum = 97,
                Remark = "授权操作",
                IsDeleted = false,
            };
            entities.Add(dictInfo23);
            DictionaryEntity dictInfo24 = new DictionaryEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "导出",
                DictValue = "5",
                DictType = "sys_oper_type",
                OrderNum = 96,
                Remark = "导出操作",
                IsDeleted = false,
            };
            entities.Add(dictInfo24);
            DictionaryEntity dictInfo25 = new DictionaryEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "导入",
                DictValue = "6",
                DictType = "sys_oper_type",
                OrderNum = 95,
                Remark = "导入操作",
                IsDeleted = false,
            };
            entities.Add(dictInfo25);
            DictionaryEntity dictInfo26 = new DictionaryEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "强退",
                DictValue = "7",
                DictType = "sys_oper_type",
                OrderNum = 94,
                Remark = "强退操作",
                IsDeleted = false,
            };
            entities.Add(dictInfo26);
            DictionaryEntity dictInfo27 = new DictionaryEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "生成代码",
                DictValue = "8",
                DictType = "sys_oper_type",
                OrderNum = 93,
                Remark = "生成代码操作",
                IsDeleted = false,
            };
            entities.Add(dictInfo27);
            DictionaryEntity dictInfo28 = new DictionaryEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "清空数据",
                DictValue = "9",
                DictType = "sys_oper_type",
                OrderNum = 92,
                Remark = "清空数据操作",
                IsDeleted = false,
                ListClass = "danger"
            };
            entities.Add(dictInfo28);



            DictionaryEntity dictInfo20 = new DictionaryEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "成功",
                DictValue = "false",
                DictType = "sys_common_status",
                OrderNum = 100,
                Remark = "正常状态",
                IsDeleted = false,
            };
            entities.Add(dictInfo20);
            DictionaryEntity dictInfo21 = new DictionaryEntity()
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                DictLabel = "失败",
                DictValue = "true",
                DictType = "sys_common_status",
                OrderNum = 99,
                Remark = "失败状态",
                IsDeleted = false,
                ListClass = "danger"
            };
            entities.Add(dictInfo21);

            return entities;
        }
    }
}
