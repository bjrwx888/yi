<template>
  <el-menu
    :default-active="activeIndex"
    class="el-menu-demo"
    mode="horizontal"
    @select="handleSelect"
    
  >
  <RouterLink to="/index">  <el-menu-item class="logo">   
    <img class="img-icon" style="width: 35px; height: 35px" src="@/assets/logo.ico"  />Yi意社区</el-menu-item></RouterLink> 
  <RouterLink to="/index"><el-menu-item index="1">主页</el-menu-item></RouterLink>
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

    <RouterLink to="/discuss"> <el-menu-item index="5">搜索</el-menu-item></RouterLink>
    <RouterLink to="/addArt"><el-menu-item index="6">分享</el-menu-item></RouterLink>
    <el-sub-menu index="7">
      <template #title>学习</template>
      <el-menu-item index="7-1">学习 one</el-menu-item>
      <el-menu-item index="7-2">学习 two</el-menu-item>
      <el-menu-item index="7-3">学习 three</el-menu-item>
    </el-sub-menu>
  </el-menu>

</template>
<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
const router = useRouter()
const activeIndex = ref('1')
const handleSelect = (key, keyPath) => {
  console.log(key, keyPath)
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