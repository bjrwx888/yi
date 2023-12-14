<template>
  <div class="common-layout">
    <el-container>
      <el-header class="common-header">
        <AppHeader />
      </el-header>
      <el-main>
        <AppBody />
      </el-main>
    </el-container>
  </div>
</template>
<script setup>
import AppHeader from "./AppHeader.vue";
import AppBody from "./AppBody.vue";
</script>

<style scoped lang="scss">
.common {
  &-header {
    width: 100%;
    display: flex;
    justify-content: center;
  }
}

.el-main {
  margin: 0;
  padding: 0;
  min-height: 10rem;
  background-color: #f0f2f5;
}
</style>
