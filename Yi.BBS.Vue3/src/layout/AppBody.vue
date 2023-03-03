
<template class="back-color"> 
   <div class="body-main">   
      <div class="content-main">
      <RouterView />
     </div>
   
   </div>


</template>

<style scoped>

.body-main{
min-height: 10rem;
min-width: 10rem;
background-color:#F0F2F5;

}
.content-main{
   max-width:1100px;
   margin: 0 auto;
}
</style>