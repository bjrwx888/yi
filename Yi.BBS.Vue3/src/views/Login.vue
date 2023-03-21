<template>
    <div class="login-wrapper">
        <h1>意社区-登录</h1>
        <div class="login-form">
            <div class="username form-item">
                <span>使用邮箱或者手机号</span>
                <input type="text" class="input-item" v-model="loginForm.userName">
            </div>
            <div class="password form-item">
                <span>密码</span>
                <input type="password" class="input-item" v-model="loginForm.password">
            </div>
            <button class="login-btn" @click="login">登 录</button>
            <button class="login-btn" @click="guestlogin">游客临时登录</button>
        </div>
        <div class="divider">
            <span class="line"></span>
            <span class="divider-text">其他方式登录</span>
            <span class="line"></span>
        </div>
        <div class="other-login-wrapper">
            <div class="other-login-item">
                <img src="@/assets/login_images/QQ.png" alt="">
            </div>
            <div class="other-login-item">
                <img src="@/assets/login_images/WeChat.png" alt="">
            </div>
        </div>
    </div>
    <!-- <h2> 登录-欢迎</h2>
    <el-input v-model="loginForm.userName" placeholder="用户名" />
    <el-input v-model="loginForm.password" placeholder="密码" show-password />
    <el-button class="login-btn" type="primary" @click="login">登录</el-button>
    <br>
    <el-button class="login-btn" type="primary" @click="guestlogin">游客临时登录</el-button> -->
</template>
<script setup>
import { reactive } from 'vue';
import { useRouter, useRoute } from 'vue-router';
import useUserStore from '@/stores/user.js'
const userStore = useUserStore();
const router = useRouter();
const route = useRoute();
const loginForm = reactive({
    userName: "",
    password: "",
    uuid: "",
    code: ""
})
const guestlogin = async () => {
    loginForm.userName = "guest";
    loginForm.password = "123456"
    await userStore.login(loginForm);
    const redirect = route.query?.redirect ?? '/index'
    router.push(redirect)
}
const login = async () => {
    const response = await userStore.login(loginForm).catch((e) => {
        loginForm.password = "";
    });
    if (response!=undefined) {
        ElMessage({
            message: `您好${loginForm.userName}，登录成功！`,
            type: 'success',
        })

        const redirect = route.query?.redirect ?? '/index'
        router.push(redirect)

    }

}
</script>
<style src="@/assets/styles/login.scss" scoped></style>
