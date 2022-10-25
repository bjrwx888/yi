<template>


<div class="head-div">
  <van-swipe
    height="100"
    class="my-swipe"
    :autoplay="3000"
    indicator-color="white"
  >
    <van-swipe-item>1</van-swipe-item>
    <van-swipe-item>2</van-swipe-item>
    <van-swipe-item>3</van-swipe-item>
    <van-swipe-item>4</van-swipe-item>
  </van-swipe>
  </div>

  <van-row class="body-row">

    <van-col span="24">   <van-grid :column-num="4">
      <van-grid-item
        v-for="value in 8"
        :key="value"
        icon="photo-o"
        text="文字"
      />
    </van-grid> </van-col>
    <van-col span="24">     <AppCard /></van-col>
    <van-col span="24"></van-col>

   
    <router-link to="/shopSearch">点击前往搜索</router-link>
    <br />
  </van-row>
</template>
<script  setup lang="ts">
import AppCard from "@/components/AppCard.vue";
</script>
<style>
.my-swipe .van-swipe-item {
  color: #fff;
  font-size: 20px;
  line-height: 150px;
  text-align: center;
  background-color: #39a9ed;
}
.body-row {
  padding: 1rem;
  padding-top: 0.2rem;
  background-color: #f4f4f4;
  margin-top: 1rem;
}
.van-swipe {
  /* margin-top: 10rem;
  margin-left: 1rem;
  margin-right: 1rem; */
}
.head-div
{
    background-color: #f4f4f4;
    padding: 1rem;
    padding-bottom: 0;
}
.van-col
{
  margin-bottom: 2rem;
}
</style>