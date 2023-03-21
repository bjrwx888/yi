<template>
  <el-menu
    :default-active="activeIndex"
    class="el-menu-demo"
    mode="horizontal"
    :ellipsis="false"
    @select="handleSelect"
    
  >
 <el-menu-item class="logo"  index="" @click="enterIndex" >   
    <img class="img-icon" style="width: 35px; height: 35px" src="@/assets/logo.ico"  />Yi意社区</el-menu-item>
 <el-menu-item index="1" @click="enterIndex">主页</el-menu-item>
    <el-sub-menu index="2">
      <template #title>学习</template>
      <el-menu-item index="2-1">学习 one</el-menu-item>
      <el-menu-item index="2-2">学习 two</el-menu-item>
      <el-menu-item index="2-3">学习 three</el-menu-item>
    </el-sub-menu>
    <el-sub-menu index="3">
      <template #title>资源</template>
      <el-menu-item index="3-1">资源 one</el-menu-item>
      <el-menu-item index="3-2">资源 two</el-menu-item>
      <el-menu-item index="3-3">资源 three</el-menu-item>
    </el-sub-menu>
    <el-sub-menu index="4">
      <template #title>问答</template>
      <el-menu-item index="4-1">问答 one</el-menu-item>
      <el-menu-item index="4-2">问答 two</el-menu-item>
      <el-menu-item index="4-3">问答 three</el-menu-item>
    </el-sub-menu>

    <div class="flex-grow" />

   <el-menu-item index="5">
    <div style="width: 350px;">
      <el-input style="width: 300px;" v-model="searchText" placeholder="全站搜索" clearable  prefix-icon="Search" />
      <el-button type="primary" plain @click="search">搜索</el-button>
    </div>
    </el-menu-item>
   
    <el-menu-item index="6" @click="enterProfile" >
        <AvatarInfo :size='30' :isSelf="true"   />
    </el-menu-item>
    <el-sub-menu index="6">
      <template #title>个人中心</template>
      <el-menu-item index="6-1" @click="enterProfile">进入个人中心</el-menu-item>
      <el-menu-item index="6-2" @click="enterProfile">其他</el-menu-item>
      <el-menu-item index="6-3" @click="logout">登出</el-menu-item>
    </el-sub-menu>

  </el-menu>

</template>
<script setup>
import AvatarInfo from '@/components/AvatarInfo.vue'
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import useUserStore from '@/stores/user.js'
const router = useRouter()
const userStore =useUserStore();
const activeIndex = ref('1')
const searchText=ref('')
const handleSelect = (key, keyPath) => {
  console.log(key, keyPath)
}
const logout= async()=>{
  ElMessageBox.confirm(`确定登出系统吗?`, "警告", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then( async() => {
    //异步
    await userStore.logOut();
    //删除成功后，跳转到主页
    router.push("/login");
    ElMessage({
      type: "success",
      message: "登出成功",
    });
  });

}
const enterIndex=()=>{
  router.push("/index");
};
const enterProfile=()=>{
  router.push("/profile");}

const search=()=>{
  var routerPer = { path: `/discuss`,query:{q:searchText.value} };
  searchText.value='';
  router.push(routerPer)
}
</script>

<style scoped>

.logo{
  min-width: 14rem;
  font-size: large;
  font-weight: 600;
}
.flex-grow {
  flex-grow: 1;
}
.img-icon
{
  margin-right: 0.5rem;
}
</style>