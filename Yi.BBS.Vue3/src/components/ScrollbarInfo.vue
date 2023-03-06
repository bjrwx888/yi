<template>
         <el-scrollbar>
    <div class="scrollbar-flex-content">
      <p v-for="item in 50" :key="item" class="scrollbar-item">
        推荐{{ item }}
      </p>
    </div>
  </el-scrollbar>
    </template>
    <style scoped>

.scrollbar-flex-content {
  display: flex;
}
.scrollbar-item {
  flex-shrink: 0;
  display: flex;
  align-items: center;
  justify-content: center;
  width: 4rem;
  height: 2.6rem;
  margin: 0 0.2rem ;
  text-align: center;
  border-radius: 4px;
  background-color:#FAFAFA;
  font-size: 14px;
}
</style>