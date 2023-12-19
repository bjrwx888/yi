<template>
  <div class="login">
    <div class="login-box">
      <div class="left"></div>
      <div class="right">
        <div class="top">
          <div class="title">{{ configStore.name }}-登录</div>
          <div class="text">若要继续，请登录</div>
        </div>
        <div class="center">
          <div class="login-form">
            <el-form ref="loginFormRef" :model="loginForm" :rules="rules">
              <div class="username form-item">
                <el-form-item prop="userName">
                  <input
                    type="text"
                    class="input-item"
                    v-model="loginForm.userName"
                    placeholder="请输入用户名"
                  />
                </el-form-item>
              </div>
              <div class="password form-item">
                <el-form-item prop="password">
                  <input
                    type="password"
                    class="input-item"
                    v-model="loginForm.password"
                    placeholder="请输入密码"
                  />
                </el-form-item>
              </div>
            </el-form>
            <div class="link">
              <div class="text" @click="handleRegister">没有账号？前往注册</div>
              <div class="text" @click="guestlogin">访客入口</div>
            </div>
            <div class="login-btn" @click="login(loginFormRef)">登 录</div>
          </div>
        </div>
        <div class="bottom">
          <div class="title">
            <div>或者</div>
            <div>其他方式登录</div>
          </div>
          <div class="icon-list">
            <div class="icon">
              <img src="@/assets/login_images/QQ.png" alt="" />
            </div>
            <div class="icon">
              <img src="@/assets/login_images/WeChat.png" alt="" />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script setup>
import { ref, reactive } from "vue";
import { useRouter, useRoute } from "vue-router";
import useConfigStore from "@/stores/config";
import useAuths from "@/hooks/useAuths";

const { loginFun } = useAuths();
const configStore = useConfigStore();
const router = useRouter();
const route = useRoute();
const loginFormRef = ref();
const rules = reactive({
  userName: [{ required: true, message: "请输入账号名", trigger: "blur" }],
  password: [{ required: true, message: "请输入密码", trigger: "blur" }],
});
const loginForm = reactive({
  userName: "",
  password: "",
  uuid: "",
  code: "",
});
const guestlogin = async () => {
  const redirect = route.query?.redirect ?? "/index";
  router.push(redirect);
};
const login = async (formEl) => {
  if (!formEl) return;
  await formEl.validate((valid) => {
    if (valid) {
      try {
        loginFun(loginForm);
      } catch (error) {
        ElMessage({
          message: error.message,
          type: "error",
          duration: 2000,
        });
      }
    }
  });
};

// 注册逻辑
const handleRegister = () => {
  console.log("注册");
};
</script>
<style scoped lang="scss">
.login {
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100%;
  height: 100%;
  background: url("@/assets/login_images/login_bg.jpg") no-repeat;
  &-box {
    display: flex;
    width: 70%;
    height: 80%;
    border-radius: 20px;
    background-color: #e0edfd;
    .left {
      width: 55%;
    }
    .right {
      display: flex;
      flex-direction: column;
      width: 45%;
      padding: 40px 30px 10px 30px;
      border-radius: 20px;
      color: #06035a;

      background-color: #fff;
      .top {
        flex: 2;
        .title {
          font-size: 30px;
          font-weight: bold;
        }
        .text {
          margin-top: 10px;
        }
      }
      .center {
        flex: 4;
        .login-form {
          width: 100%;
          height: 100%;
          display: flex;
          flex-direction: column;
          justify-content: space-between;
          .input-item {
            width: 100%;
            height: 45px;
            outline: none;
            border: 2px solid #dde0df;
            border-radius: 5px;
            padding: 0 10px;
            &:hover {
              outline: none;
            }
          }
          .login-btn {
            cursor: pointer;
            width: 100%;
            height: 45px;
            color: #fff;
            text-align: center;
            line-height: 50px;
            border-radius: 5px;
            background-color: #2282fe;
          }
          .link {
            margin-bottom: 10px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            .text {
              cursor: pointer;
            }
          }
          .visitor {
            margin-top: 10px;
          }
        }
      }
      .bottom {
        margin-top: 20px;
        flex: 2;
        width: 100%;
        display: flex;
        flex-direction: column;
        justify-content: flex-start;
        .title {
          > div {
            text-align: center;
            margin: 10px;
          }
        }
        .icon-list {
          margin-top: 10px;
          width: 100%;
          display: flex;
          justify-content: center;
          .icon {
            width: 20px;
            height: 20px;
            margin: 0 10px;
            img {
              width: 100%;
              height: 100%;
            }
          }
        }
      }
    }
  }
}
</style>
