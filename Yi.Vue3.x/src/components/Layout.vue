<template>
  <div class="common-layout">
    <el-container>
      <el-header> <AppHeader></AppHeader></el-header>
      <el-main > <AppMain></AppMain></el-main>
      <el-footer> <AppFoot></AppFoot></el-footer>
    </el-container>
  </div>
</template>

<script lang="ts" setup>
import AppHeader from "./AppHeader.vue";
import AppFoot from "./AppFoot.vue";
import AppMain from "./AppMain.vue";
</script>
<style lang="scss" scope>
.common-layout {
  .el-header,
  .el-footer {
    background-color: #b3c0d1;
    color: var(--el-text-color-primary);
    text-align: center;
    line-height: 60px;

    padding: 0;
  }

  .el-footer {
          background-color: #F0F2F5;
    line-height: 60px;
  }



  .el-main {
    background-color: #F0F2F5;
    color: var(--el-text-color-primary);
    text-align: center;
     padding: 25px;
  }

  body > .el-container {
    margin-bottom: 40px;
  }

}
</style>

