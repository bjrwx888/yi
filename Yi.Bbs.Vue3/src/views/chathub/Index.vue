<template>
  <div class="body">

    <div class="left">
      <div class="icon">
        <img src="@/assets/chat_images/icon.jpg">
      </div>
      <ul class="top-icon">
        <li><img src="@/assets/chat_images/wechat.png" /></li>
        <li><img src="@/assets/chat_images/addressBook.png" /></li>
        <li><img src="@/assets/chat_images/collection.png" /></li>
        <li><img src="@/assets/chat_images/file.png" /></li>
        <li><img src="@/assets/chat_images/friend.png" /></li>
        <li><img src="@/assets/chat_images/line.png" /></li>
        <li><img src="@/assets/chat_images/look.png" /></li>
        <li><img src="@/assets/chat_images/sou.png" /></li>
      </ul>
      <ul class="bottom-icon">
        <li><img src="@/assets/chat_images/mini.png" /></li>
        <li><img src="@/assets/chat_images/phone.png" /></li>
        <li><img src="@/assets/chat_images/other.png" /></li>
      </ul>

    </div>

    <div class="middle">
      <div class="header">
        <div class="header-div">
          <div class="search">
            <img src="@/assets/chat_images/search.png" />
            <span>搜索</span>
          </div>
          <button type="button"> <img src="@/assets/chat_images/add.png" /></button>
        </div>
      </div>
      <div class="user-list">
        <div class="user-div" style="background-color: #C8C8CA;">
          <div class="user-div-left">
            <img src="@/assets/chat_images/friendicon.jpg" />
            <div class="user-name-msg">
              <p class="font-name">橙子</p>
              <p class="font-msg">现在感觉怎么样</p>
            </div>
          </div>
          <div class=" user-div-right">
            10:28
          </div>

        </div>


        <div v-for="i in 100" :key="i" class="user-div">
          <div class="user-div-left">
            <img src="@/assets/chat_images/friendicon.jpg" />
            <div class="user-name-msg">
              <p class="font-name">橙子</p>
              <p class="font-msg">现在感觉怎么样</p>
            </div>
          </div>
          <div class=" user-div-right">
            10:28

          </div>
        </div>
      </div>
    </div>

    <div class="right">
      <div class="header">
        <div class="header-left">橙子</div>
        <div class="header-right">
          <div>
            <ul>
              <li><img src="@/assets/chat_images/fixed.png" /></li>
              <li><img src="@/assets/chat_images/min.png" /></li>
              <li><img src="@/assets/chat_images/max.png" /></li>
              <li><img src="@/assets/chat_images/close.png" /></li>
            </ul>
          </div>
          <div class="more"><img src="@/assets/chat_images/other2.png" /></div>


        </div>

      </div>
      <div class="content">
        <div v-for="i in 100" :key="i">
          <div class="content-others content-common">
            <img src="@/assets/chat_images/friendicon.jpg" />
            <div class="content-others-msg content-msg-common  ">感觉还可以</div>
          </div>
          <div class="content-myself content-common">
            <div class="content-myself-msg content-msg-common ">现在感觉怎么样</div>
            <img src="@/assets/chat_images/icon.jpg" />
          </div>

        </div>

      </div>
      <div class="bottom">
        <div class="bottom-tool">

            <ul class="ul-left">
              <li><img src="@/assets/chat_images/emoji.png" /></li>
              <li><img src="@/assets/chat_images/sendFile.png" /></li>
              <li><img src="@/assets/chat_images/screenshot.png" /></li>
              <li><img src="@/assets/chat_images/chatHistory.png" /></li>
            </ul>

            <ul class="ul-right">
              <li><img src="@/assets/chat_images/landline.png" /></li>
              <li><img src="@/assets/chat_images/videoChat.png" /></li>
            </ul>
          
            </div>
            <div class="bottom-input" contenteditable="true">

            </div>

            <div class="bottom-send">
              <button>
                发送(S)
              </button>

            </div>
          </div>
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

    img {
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
    padding: 26px 0 26px 0;

    .header-div {
      background-color: #F7F7F7;
      height: 30px;
      width: 338px;
      margin: auto;
      display: flex;
      justify-content: space-between;

      .search {
        width: 300px;
        height: 100%;
        background-color: #E2E2E2;
        border-radius: 5px;
        padding: 5px;

        img {
          height: 16px;
          width: 16px;
        }

        span {
          margin-left: 10px;
          font-size: 14px;
          color: #818181;
          margin-bottom: 5px;
          vertical-align: top;
        }
      }

      button {
        margin-left: 10px;
        width: 30px;
        background-color: #E2E2E2;
        border-radius: 5px;
        padding: 5px 3px;

        img {
          height: 100%;
          width: 100%;
        }
      }
    }

  }

  .user-list::-webkit-scrollbar-thumb {
    background-color: #BEBCBA;
    /* 滚动条滑块颜色 */
  }

  .user-list::-webkit-scrollbar {
    width: 8px;
    /* 滚动条宽度 */
    height: 10px;
    /* 滚动条高度 */
  }

  .user-list {
    height: calc(100% - 75px);


    overflow-y: auto;

    /* 只启用垂直方向滚动条 */
    .user-div {
      display: flex;
      justify-content: space-between;
      height: 80px;
      width: 100%;
      background-color: #EAE8E7;
      padding: 16px;

      &-left {

        height: 100%;
        display: flex;

        .user-name-msg {
          display: flex;
          flex-direction: column;
          justify-content: space-between;
          padding: 0 10px;

          .font-name {
            font-size: 20px;
          }

          .font-msg {
            font-size: 15px;
            color: #999999;
          }
        }

        img {
          height: 100%;

        }
      }

      &-right {
        font-size: 15px;
        color: #999999;
      }
    }

    .user-div:hover {
      background-color: #D9D8D8;
    }
  }
}

.right {
  background-color: #f5f5f5;
  width: 950px;

  .header {
    height: 75px;
    background: #f7f7f7;
    border: 1px solid #e7e7e7;
    display: flex;
    justify-content: space-between;

    .header-left {
      padding: 25px;
      font-size: 25px;
      align-content: center;
    }

    .header-right {
      ul {
        display: flex;

        li {
          padding: 8px 12.5px 10px 12.5px;

          img {
            height: 15px;
            width: 15px;
          }
        }
      }

      .more {
        padding: 0px 12.5px;
        display: flex;
        justify-content: flex-end;

        img {
          height: 18px;
          width: 18px;
        }
      }
    }
  }

  .content {
    overflow-y: auto;
    /* 只启用垂直方向滚动条 */
    height: 555px;
    padding: 20px 40px;
  }

  .bottom {
    height: calc(100% - 630px);
    background: #f7f7f7;
    border-top: 1.5px solid #e7e7e7;
    padding: 15px 35px;

    &-tool {
      display: flex;
    justify-content: space-between;
      height: 22px;
      .ul-left {
          display: flex;

          li {
            width: 22px;
            height: 22px;
            margin-right: 20px;
          }

          img {
            width: 100%;
            height: 100%;
          }
        }
   
      .ul-right {
          display: flex;

          li {
            width: 22px;
            height: 22px;
            margin-right: 20px;
          }

          img {
            width: 100%;
            height: 100%;
          }
        }
    
      }

    &-input {
      height: 70px;
      width: 100%;
      overflow-y: auto;
      padding: 10px 0;
      font-size: 18px;
    }

    &-send {
      display: flex;
      justify-content: flex-end;

      button {
        width: 126px;
        height: 40px;
        background-color: #E9E9E9;
        color: #06AE56;
        border-radius: 5px;
        font-size: 18px;
      }

      button:hover {
        background-color: #D2D2D2;

      }
    }
  }
}

.top-icon {
  margin-top: 32px;

  li {
    cursor: pointer;
    text-align: center;
    margin-bottom: 24px;

    img {
      height: 24px;
      width: 24px;

    }

  }
}

.bottom-icon {
  margin-top: 125px;

  li {
    cursor: pointer;
    text-align: center;
    margin-bottom: 24px;

    img {
      height: 24px;
      width: 24px;

    }

  }
}



.content-common {
  display: flex;

  img {
    height: 45px;
    width: 45px;
  }
}

.content-msg-common {
  display: flex;
  align-content: center;
  flex-wrap: wrap;
  position: relative;

  margin: 0 15px;
  padding: 5px 15px;
  text-align: center;
  font-size: 18px;
  border-radius: 5px;
}


.content-myself {
  justify-content: flex-end;
}

.content-myself-msg:hover {

  background-color: #89D961;
}

.content-myself-msg {
  background-color: #95EC69;
}

.content-myself-msg:hover:after {
  border-left: 10px solid #89D961;
}


.content-others {
  justify-content: flex-start;
}

.content-others-msg {
  background-color: #FFFFFF;
}

.content-others-msg:hover {
  background-color: #EBEBEB;
}

.content-others-msg:hover:after {
  border-right: 10px solid #EBEBEB;
}

.content-myself-msg:after {
  content: '';
  position: absolute;
  width: 0;
  height: 0;
  /* 箭头靠右边 */
  top: 13px;
  right: -10px;
  border-top: 10px solid transparent;
  border-bottom: 10px solid transparent;
  border-left: 10px solid #97EC6F;

}

.content-others-msg:after {
  /* 箭头靠左边 */
  content: '';
  position: absolute;
  width: 0;
  height: 0;
  top: 13px;
  left: -10px;
  border-top: 10px solid transparent;
  border-bottom: 10px solid transparent;
  border-right: 10px solid #FFFFFF;

}
</style>