<template > 
  <van-tabs   v-model:active="active" offset-top="0" :sticky="true" class="tabs">
    <van-tab title="关注" to="/follow"></van-tab>
    <van-tab title="推荐" to="/recommend"></van-tab>
    <van-tab title="广场"></van-tab>
    <van-tab title="视频"></van-tab>

    <router-view />
  </van-tabs>
</template>
<script setup lang="ts">
import { ref } from "vue";
const active = ref(1);
</script>
<style scoped>
.tabs{
  width: 25rem;
}
.view{
  margin: auto;
}
</style>
