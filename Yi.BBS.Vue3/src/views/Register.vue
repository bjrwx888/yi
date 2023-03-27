<template>
    <div class="login-wrapper">
        <h1>{{configStore.name}}-注册</h1>
        <div class="login-form">
            <div class="username form-item">
                <span>登录账号</span>
                <input type="text" class="input-item" v-model="loginForm.userName">
            </div>
            <div class="username form-item">
                <span>手机号</span>
                <input type="text" class="input-item" v-model="loginForm.userName">
            </div>
            <div class="password form-item">
                <span>密码</span>
                <input type="password" class="input-item" v-model="loginForm.password">
            </div>
            <button class="login-btn" @click="login">注册</button>
        </div>
        <div class="divider">
            <span class="line"></span>
            <span class="divider-text">其他方式注册</span>
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
</template>
<script setup>
import { reactive } from 'vue';
import { useRouter, useRoute } from 'vue-router';
import useUserStore from '@/stores/user.js'
import useConfigStore from "@/stores/config";
const  configStore= useConfigStore();
const userStore = useUserStore();
const router = useRouter();
const route = useRoute();
const loginForm = reactive({
    userName: "",
    password: "",
    uuid: "",
    code: ""
})

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
