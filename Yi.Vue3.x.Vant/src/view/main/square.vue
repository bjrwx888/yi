<template >


  <van-row class="test ">
    <van-col span="24">
      <van-list
  v-model:loading="loading"
  :finished="finished"
  finished-text="没有更多了"
  @load="onLoad"
>
  <van-cell v-for="item in list" :key="item" :title="item" />
</van-list>
    </van-col>

  </van-row>

  <van-col span="24"></van-col>
</template>
<style scoped>
</style>
<script lang="ts" setup>
import {ref} from 'vue'

  const list = ref([]);
    const loading = ref(false);
    const finished = ref(false);

    const onLoad = () => {
      console.log("执行")
      // 异步更新数据
      // setTimeout 仅做示例，真实场景中一般为 ajax 请求
      setTimeout(() => {
        for (let i = 0; i < 100; i++) {
          list.value.push(list.value.length + 1);
        }

        // 加载状态结束
        loading.value = false;

        // 数据全部加载完成
        if (list.value.length >= 1000) {
          finished.value = true;
        }
      }, 1);
    };

</script>