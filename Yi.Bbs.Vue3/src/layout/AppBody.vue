<template class="back-color">
  <div class="content-main">
    <RouterView />
  </div>
</template>

<style scoped>
.body-main {
  min-height: 10rem;
  min-width: 10rem;
  background-color: #f0f2f5;
}
.content-main {
  margin: 0 auto;
  display: flex;
  justify-content: center;
  width: 1300px;
  min-height: 1150px;
}
</style>
