<template>
<div class="body-div" id="body">




  <div  class="left">
    <video class="videoBG" controls autoplay playsinline loop disablepictureinpicture  poster="/loginBg.png">
    <source src="/loginBg.mp4" type="video/mp4">
    您的浏览器不支持 video 标签。
</video>
</div>


<div class="right">
  <RouterView />
</div>


</div>


  </template>

  <script setup>
import { onMounted } from 'vue';

const screenSize=()=>{

  var screenHeight=document.documentElementclientHeight;
		var screenWidth=document.documentElement.clientWidth;
		var body=document.getElementById('body');
		body.style.width=screenWidth+"px";
		body.style.height=screenHeight+"px";
}
onMounted(()=>{
  screenSize();
  window.onresize = () => (() => {
    screenSize();
    })();
})

</script>
<style scoped>
.left
{
  width: 100%;
  background-color:#00020E
}
.right
{
  max-width: 520px;
  width:40% ;
  background-color: blue;
}
.body-div
{
  display: flex;
  justify-content: space-between;
  width: 100%;

}
.videoBG
{
width:100% ;
}

    video::-webkit-media-controls-fullscreen-button {
        display: none;
    }

    video::-webkit-media-controls-play-button {
        display: none;
    }

    video::-webkit-media-controls-timeline {
        display: none;
    }

    video::-webkit-media-controls-current-time-display{
        display: none;            
    }

    video::-webkit-media-controls-time-remaining-display {
        display: none;            
    }

    video::-webkit-media-controls-mute-button {
        display: none;            
    }
    video::-webkit-media-controls-toggle-closed-captions-button {
        display: none;            
    }

    video::-webkit-media-controls-volume-slider {
        display: none;            
    }

    video::-webkit-media-controls-enclosure{ 
        display: none;
    }
</style>
