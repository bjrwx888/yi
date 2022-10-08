<template>
    <van-tabbar v-model="active" active-color="#ee0a24" inactive-color="#000">
  <van-tabbar-item icon="home-o" to="/">主页</van-tabbar-item>
  <van-tabbar-item icon="search">发现</van-tabbar-item>

  <van-tabbar-item @click="show = true">
    <template #icon="props">
        <van-icon class="add" name="add-square" color="#1989fa" size="3rem" />
      <!-- <img :src="props.active ? icon.active : icon.inactive" /> -->
    </template>
  </van-tabbar-item>

  <van-tabbar-item icon="friends-o">商城</van-tabbar-item>
  <van-tabbar-item icon="setting-o" to="/my">我的</van-tabbar-item>
</van-tabbar>

<van-action-sheet v-model:show="show" >


    <van-button class="btn1 btn " type="primary" >发图文</van-button>
  <van-button class="btn" type="primary">发视频</van-button>
  <van-button class="btn" type="primary">发文章</van-button>
  <van-button class="btn" type="primary">发二手</van-button>


  
  <van-icon  class="icon" name="cross" size="1.5rem" @click="show=false"/>


</van-action-sheet>
</template>
<script setup lang="ts">
import { ref } from 'vue'
const active = ref(0);
const show = ref(false);
</script>
<style>
.btn1
{
  margin-top: 3rem !important;
}
.btn{
  border-radius:0.5rem;
  height: 4rem;
  width:90%;
margin: 0.5rem 1rem 0.5rem 1rem;
}
  .content {
    padding: 16px 16px 160px;
  }
  .icon{
    margin-bottom: 1.5rem;
  }
  .add{
    margin-top: 0.2rem;
  }
</style>
