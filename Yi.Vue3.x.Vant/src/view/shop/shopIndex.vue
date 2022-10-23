<template>
    这里是商城主页
    <van-swipe class="my-swipe" :autoplay="3000" indicator-color="white" lazy-render>
  <van-swipe-item>1</van-swipe-item>
  <van-swipe-item>2</van-swipe-item>
  <van-swipe-item>3</van-swipe-item>
  <van-swipe-item>4</van-swipe-item>
</van-swipe>
<van-grid :column-num="4">
  <van-grid-item v-for="value in 8" :key="value" icon="photo-o" text="文字" />
</van-grid>
    <router-link to="/shopSearch">点击前往搜索</router-link>
    <br>
</template>
<style>
  .my-swipe .van-swipe-item {
    color: #fff;
    font-size: 20px;
    line-height: 150px;
    text-align: center;
    background-color: #39a9ed;
  }
</style>