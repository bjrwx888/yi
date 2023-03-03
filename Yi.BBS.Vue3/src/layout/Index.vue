<template>
  <div class="common-layout">
    <el-container>
      <el-header>

        <AppHeader />

      </el-header>
      <el-main>
       <AppBody />
      </el-main>
    </el-container>
  </div>
</template>
<script setup>
import AppHeader from "./AppHeader.vue"
import AppBody from "./AppBody.vue"
</script>

<style scoped>
.el-main{
  margin: 0;
padding: 0;
}
</style>