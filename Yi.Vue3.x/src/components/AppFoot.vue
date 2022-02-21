<template >
<div >
    <div>
  <span>
    <el-icon :size="14"> <edit /> </el-icon>支持项目</span
  >
  <span>
    <el-icon :size="14"> <edit /> </el-icon>联系作者</span
  >
  <span>
    <el-icon :size="14"> <edit /> </el-icon>免责声明</span
  >
</div>


  <div class="el-footer"> <span>
    <el-icon :size="14"> <edit /> </el-icon>免责声明</span
  ></div>
</div>
</template>
<script setup >
import { Edit } from "@element-plus/icons-vue";
</script>
<style scoped>

.el-icon{
    margin-right: 4px;
}
span{
    margin-right: 40px;
}
  .el-footer {
          background-color: #F0F2F5;
    line-height: 60px;
  }
</style>