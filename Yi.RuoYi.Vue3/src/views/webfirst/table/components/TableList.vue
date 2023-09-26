<template>
 
      <h5 class="mb-2">   {{table}}</h5>
      <el-menu
        default-active="2"
        class="el-menu-vertical-demo"
        @open="handleOpen"
        @close="handleClose"
      >

 <el-menu-item index="1">
          <el-icon><setting /></el-icon>
          <span>Navigator Four</span>
        </el-menu-item>
         <el-menu-item index="2">
          <el-icon><setting /></el-icon>
          <span>Navigator Four</span>
        </el-menu-item>
         <el-menu-item index="3">
          <el-icon><setting /></el-icon>
          <span>Navigator Four</span>
        </el-menu-item>
         <el-menu-item index="4">
          <el-icon><setting /></el-icon>
          <span>Navigator Four</span>
        </el-menu-item>
        <el-menu-item index="5">
          <el-icon><setting /></el-icon>
          <span>Navigator Four</span>
        </el-menu-item>
      </el-menu>
</template>

<script lang="ts" setup>


import useTable from '../hooks/useTable'
const {table}=useTable();

const handleOpen = (key, keyPath) => {
  console.log(key, keyPath)
}
const handleClose = (key, keyPath) => {
  console.log(key, keyPath)
}
</script>
