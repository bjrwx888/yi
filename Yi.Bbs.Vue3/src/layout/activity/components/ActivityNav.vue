<template>
        <el-menu
        default-active="1"
        class="el-menu-vertical-demo"
        @open="handleOpen"
        @close="handleClose"
      >
      <el-menu-item index="1">
        <el-icon><Present /></el-icon>
          <span>每日签到</span>
        </el-menu-item>
        <el-menu-item index="2">
            <el-icon><Memo /></el-icon>
          <span>任务列表</span>
        </el-menu-item>
        <el-menu-item index="3">
            <el-icon><HelpFilled /></el-icon>
          <span>大转盘</span>
        </el-menu-item>
        <el-menu-item index="4">
            <el-icon><Sunrise /></el-icon>
          <span>娱乐城</span>
        </el-menu-item>
      </el-menu>

</template>
<script setup>
const handleOpen = (key, keyPath) => {
  console.log(key, keyPath)
}
const handleClose = (key, keyPath) => {
  console.log(key, keyPath)
}
</script>
<style lang="scss" scoped>
.el-menu-item
{
    justify-content: center;
}
.el-menu{
    border-right-width: 0px !important;

}
</style>