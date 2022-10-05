<template>
    <van-tabbar v-model="active" active-color="#ee0a24" inactive-color="#000">
  <van-tabbar-item icon="home-o" to="/">主页</van-tabbar-item>
  <van-tabbar-item icon="search">发现</van-tabbar-item>

  <van-tabbar-item>
    <template #icon="props">
        <van-icon name="add-square" color="#1989fa" size="3rem" />
      <!-- <img :src="props.active ? icon.active : icon.inactive" /> -->
    </template>
  </van-tabbar-item>

  <van-tabbar-item icon="friends-o">商城</van-tabbar-item>
  <van-tabbar-item icon="setting-o" to="/my">我的</van-tabbar-item>
</van-tabbar>
</template>
<script setup lang="ts">
import { ref } from 'vue'
const active = ref(0);
</script>
