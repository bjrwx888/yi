<template>
  <div class="app-container" style="width: 1300px">
    <!-- 老版用户信息页面 -->
    <!-- <el-row :gutter="20">
      <el-col :span="6" :xs="24">
        <el-card class="box-card">
          <template v-slot:header>
            <div class="clearfix">
              <span>个人信息</span>
            </div>
          </template>
          <div>
            <div class="text-center">
              <userAvatar :user="state.user" />
            </div>
            <ul class="list-group list-group-striped">
              <li class="list-group-item">
                <el-icon><User /></el-icon> 账号
                <div class="pull-right">{{ state.user.userName }}</div>
              </li>
              <li class="list-group-item">
                <el-icon><Phone /></el-icon> 手机号码
                <div class="pull-right">{{ state.user.phone }}</div>
              </li>
              <li class="list-group-item">
                <el-icon><Message /></el-icon> 用户邮箱
                <div class="pull-right">{{ state.user.email }}</div>
              </li>
              <li class="list-group-item">
                <el-icon><Avatar /></el-icon> 所属角色
                <div class="pull-right">
                  <span v-for="role in state.roles" :key="role.id"
                    >{{ role.roleName }} /</span
                  >
                </div>
              </li>
              <li class="list-group-item">
                <el-icon><Stopwatch /></el-icon> 注册日期
                <div class="pull-right">{{ state.user.creationTime }}</div>
              </li>
            </ul>
          </div>
        </el-card>
      </el-col>
      <el-col :span="18" :xs="24">
        <el-card>
          <template v-slot:header>
            <div class="clearfix">
              <span>基本资料</span>
            </div>
          </template>
          <el-tabs v-model="activeTab">
            <el-tab-pane label="基本资料" name="userinfo">
              <userInfo :user="state.user" />
            </el-tab-pane>
            <el-tab-pane label="修改密码" name="resetPwd">
              <resetPwd />
            </el-tab-pane>
            <el-tab-pane label="第三方快捷登录" name="accountSetting">
              <accountSet />
            </el-tab-pane>
          </el-tabs>
        </el-card>
      </el-col>
    </el-row> -->

    <!-- 新版用户信息页面 -->
    <el-row>
      <el-col :span="24">
        <div class="div-top">
          <div class="div-top-user">
            <div class="user-icon">用户头像</div>
            <div class="user-info">
              <div class="user-nick">
                <div class="user-nick-left">CCNetCore</div>
                <div class="user-nick-right">其他</div>
              </div>

              <div class="user-remark">
                <span>100</span> 总访问 | <span>200</span> 排名 |
                <span>36</span> 主题 | <span>836</span> 好友
              </div>
              <div>
                <p>个人简介：你好</p>
                <p>个人简介：你好</p>
                <p>注册时间：2020-12-02</p>
                <p>个人简介：你好,很好，非常的好</p>
              </div>
            </div>
          </div>

          <div class="user-edit">
            <div class="user-info-bottom">

                <el-tabs v-model="activeTab" class="user-edit-tab">
                  <el-tab-pane label="基本资料" name="userinfo">
                    <userInfo :user="state.user" />
                  </el-tab-pane>
                  <el-tab-pane label="修改密码" name="resetPwd">
                    <resetPwd />
                  </el-tab-pane>
                  <el-tab-pane label="第三方快捷登录" name="accountSetting">
                    <accountSet />
                  </el-tab-pane>
                </el-tabs>
    
            </div>
          </div>
        </div>
      </el-col>
    </el-row>

    <el-row class="div-bottom">
      <el-col :span="5" class="div-bottom-left"> </el-col>
      <el-col :span="19" class="div-bottom-right">
        <div class="div-bottom-right-content">个人主题内容</div>
      </el-col>
    </el-row>
  </div>
</template>

<script setup name="Profile">
import userAvatar from "./UserAvatar.vue";
import userInfo from "./UserInfo.vue";
import resetPwd from "./ResetPwd.vue";
import accountSet from "./AccountSetting.vue";
import { getUserProfile } from "@/apis/userApi.js";
import { onMounted, ref, reactive } from "vue";

const activeTab = ref("userinfo");
const state = reactive({
  user: {},
  dept: {},
  roles: [],
  roleGroup: {},
  postGroup: {},
});

function getUser() {
  getUserProfile().then((response) => {
    const res = response.data;
    state.user = res.user;
    state.dept = res.dept;
    state.roles = res.roles;
    state.roleGroup = res.roleGroup;
    state.postGroup = res.postGroup;
  });
}
onMounted(() => {
  getUser();
});
</script>
<style scoped  lang="scss">
.pull-right {
  float: right !important;
}
.list-group-striped > .list-group-item {
  border-left: 0;
  border-right: 0;
  border-radius: 0;
  padding-left: 0;
  padding-right: 0;
}

.list-group {
  padding-left: 0px;
  list-style: none;
}

.list-group-item {
  border-bottom: 1px solid #e7eaec;
  border-top: 1px solid #e7eaec;
  margin-bottom: -1px;
  padding: 11px 0px;
  font-size: 13px;
}

.app-container {
  padding: 20px;
}
.text-center {
  display: flex;
  justify-content: center;
  margin-bottom: 10px;
}

$topHeight: 550px;
$userHeight: 150px;
$remarkHeight: $topHeight - 150;
.div-top {
  // padding-bottom: 10px;
  background-color: #ffffff;
  min-height: $topHeight;
  margin-top: 30px;
  font-size: 14px;
  color: #555666;
  &-user {
    display: flex;
    width: 100%;
    height: $userHeight;
    .user-icon {
      flex: 1;
      background-color: aqua;
      height: $userHeight;
    }
    .user-info {
      flex: 9;
      background-color: bisque;
      height: $userHeight;
      .user-nick {
        display: flex;
        justify-content: space-between;
        padding-top: 5px;
        padding-bottom: 5px;
        &-left {
          color: #222226;
          font-size: larger !important;
          font-weight: 500 !important;
        }
      }

      .user-remark span {
        font-size: larger;
        font-weight: bold;
        color: black;
      }

      .user-remark  p {
      margin-bottom: 15px;
    }
    }
  }
  .user-edit {
    height: $remarkHeight;
    flex: 1 0 auto;

    padding-left: 10%;
    background-color: burlywood;

  }
}
.user-edit-tab
{
width: 80%;
}

.div-bottom {
  margin-top: 20px;

  &-left {
    height: 1000px;
    background-color: cornsilk;
  }
  &-right {
    height: 1000px;
    background-color: #f0f2f5;
    padding-left: 20px;
    &-content {
      height: 100%;
      width: 100%;
      background-color: darkorange;
    }
  }
}
</style>
