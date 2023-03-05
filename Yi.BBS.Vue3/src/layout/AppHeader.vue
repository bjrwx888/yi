<template>
  <el-menu
    :default-active="activeIndex"
    class="el-menu-demo"
    mode="horizontal"
    @select="handleSelect"
    
  >
  <RouterLink to="/index">  <el-menu-item class="logo" @click="enterIndex"> Logo</el-menu-item></RouterLink> 
  <RouterLink to="/index"><el-menu-item index="1" @click="enterIndex">主页</el-menu-item></RouterLink>
    <el-sub-menu index="2">
      <template #title>学习</template>
      <el-menu-item index="2-1">item one</el-menu-item>
      <el-menu-item index="2-2">item two</el-menu-item>
      <el-menu-item index="2-3">item three</el-menu-item>
    </el-sub-menu>
    <el-sub-menu index="3">
      <template #title>资源</template>
      <el-menu-item index="3-1">item one</el-menu-item>
      <el-menu-item index="3-2">item two</el-menu-item>
      <el-menu-item index="3-3">item three</el-menu-item>
    </el-sub-menu>
    <el-sub-menu index="4">
      <template #title>问答</template>
      <el-menu-item index="4-1">item one</el-menu-item>
      <el-menu-item index="4-2">item two</el-menu-item>
      <el-menu-item index="4-3">item three</el-menu-item>
    </el-sub-menu>

    <div class="flex-grow" />

    <RouterLink to="/discuss"> <el-menu-item index="5">搜索</el-menu-item></RouterLink>
    <el-menu-item index="6">学习</el-menu-item>
    <el-sub-menu index="7">
      <template #title>分享</template>
      <el-menu-item index="7-1">item one</el-menu-item>
      <el-menu-item index="7-2">item two</el-menu-item>
      <el-menu-item index="7-3">item three</el-menu-item>
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
}
.flex-grow {
  flex-grow: 1;
}
</style>