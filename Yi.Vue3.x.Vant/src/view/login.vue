<template>
    <div class="div-top">
        你好
   </div>
   <div class="div-bottom">
      <!-- <van-field v-model="value" label="文本" placeholder="请输入用户名" /> -->
   </div>
</template>
<script setup lang="ts">
import {ref} from 'vue';
const value=ref("")

</script>
<style scoped>
.div-top{
    background-color: aqua;
    position:absolute;
    top: 0;
    left: 0;
   right: 0;
   bottom: 60%;
}
.div-bottom{
   background-color: bisque;
   position:absolute;
    top: 30%;
    left: 0;
   right: 0;
   bottom: 0;
   border-radius: 4rem 4rem 0rem 0rem;
   /* min-height: 70%; */
}

</style>