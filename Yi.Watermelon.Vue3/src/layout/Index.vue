<script setup>

</script>

<template>
  <div class="body">
  <div class="head"></div>
  <div class="content">
    <router-view></router-view>
    <router-view name="market"></router-view>
  </div>
  <div class="bottom"></div>
  </div>
</template>

<style scoped>
.body{
  display: flex;
  background-color: #cdcdcd;
}
</style>