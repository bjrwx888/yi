<template>
<div class="body-div" id="body">

  <div  class="left">
    <video class="videoBG"  autoplay playsinline loop disablepictureinpicture  poster="/loginBg.png">
    <!-- <source src="/loginBg.mp4" type="video/mp4"> -->
    </video>
  </div>

  <div class="right">
    <div class="right-div">
    <RouterView />
  </div>
  </div>

</div>


  </template>

  <script setup>
import { onMounted } from 'vue';

</script>
<style scoped>
.left
{

 background-color: #010413;
}
.right
{

}
.right-div
{
margin-top: 60%;
padding: 2rem;
}
.body-div
{

display: flex;
justify-content: space-between;
height: 930px;
}
.videoBG
{

width:100% ;
}

</style>
