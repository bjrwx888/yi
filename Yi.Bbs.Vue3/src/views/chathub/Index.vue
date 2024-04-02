<template>
  <div class="body">

    <div class="left">
      <div class="icon">
        <img src="@/assets/chat_images/icon.jpg">
      </div>
<ul class="top-icon">
  <li><img src="@/assets/chat_images/wechat.png"></li>
  <li><img src="@/assets/chat_images/addressBook.png"></li>
  <li><img src="@/assets/chat_images/collection.png"></li>
  <li><img src="@/assets/chat_images/file.png"></li>
  <li><img src="@/assets/chat_images/friend.png"></li>
  <li><img src="@/assets/chat_images/line.png"></li>
  <li><img src="@/assets/chat_images/look.png"></li>
  <li><img src="@/assets/chat_images/sou.png"></li>
</ul>
<ul class="bottom-icon">
  <li><img src="@/assets/chat_images/mini.png"></li>
  <li><img src="@/assets/chat_images/phone.png"></li>
  <li><img src="@/assets/chat_images/other.png"></li>
</ul>

    </div>

    <div class="middle">
      <div class="header">
<div class="search"></div>

      </div>
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
  padding: 46px 10px 0 10px;

  .icon {
    background-color: burlywood;
    height: 46px;
    width: 46px;
    img{
      height: 100%;
      width: 100%;
    }
  }
}

.middle {
  background-color: #dadbdc;
  width: 380px;

  .header {
    height: 75px;
    background: #f7f7f7;
    padding: 30px 0 30px 0  ;
    .search{
      margin: auto;
      background-color: #E2E2E2;
      height: 30px;
      width: 338px;
    }
  }

  .user-list {}
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

.top-icon
{
  margin-top: 32px;
  li{
    cursor: pointer;
    text-align: center;
   margin-bottom: 24px;
    img{
      height: 24px;
      width: 24px;
     
    }

  }
}
.bottom-icon
{margin-top: 125px;
  li{
    cursor: pointer;
    text-align: center;
   margin-bottom: 24px;
    img{
      height: 24px;
      width: 24px;
     
    }

  }
}
</style>