<template>
  <div class="body">

      <div class="left"></div>

      <div class="middle">
        <div class="header"></div>
        <div class="user-list"></div>
      </div>

      <div class="right">
        <div class="header"></div>
        <div class="content"></div>
        <div class="bottom"></div>
      </div>
    </div>

</template>
<style scoped lang="scss">
.body {
    height: 790px;
    width: 1400px;
  display: flex;
  justify-content: center;
  box-shadow: 0px 0px 5px rgba(0, 0, 0, 0.2);
}
.left {
  background-color: #2a2a2a;
  width: 70px;
}

.middle {
  background-color: #dadbdc;
  width: 380px;
  .header {
    height: 75px;
    background: #f7f7f7;
  }
  .user-list {
  }
}
.right {
  background-color: #f5f5f5;
  width: 950px;
  .header {
    height: 75px;
    background: #f7f7f7;
    border: 1px solid #e7e7e7;
  }
  .content {
    height: 580px;
  }
  .bottom {
    height: calc(100% - 655px);
    background: #f7f7f7;
    border: 1px solid #e7e7e7;
  }
}
</style>