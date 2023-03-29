<template>
    <div class="login-wrapper">
        <h1>{{configStore.name}}-登录</h1>
        <div class="login-form">

            <el-form  ref="loginFormRef" v-model="loginForm"   :rules="rules" >
             
            <div class="username form-item">
                <el-form-item  prop="userName">
                <span>使用账号</span>
                <input type="text" class="input-item" v-model="loginForm.userName">
            </el-form-item>
            </div>
          
            <div class="password form-item">
                <el-form-item  prop="password">
                <span>密码</span>
                <input type="password" class="input-item" v-model="loginForm.password">
            </el-form-item>
            </div>
        </el-form>
            <RouterLink to="/register"  > 没有账号？前往注册</RouterLink>
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
import useConfigStore from "@/stores/config";
const  configStore= useConfigStore();
const userStore = useUserStore();
const router = useRouter();
const route = useRoute();

const rules = reactive({
  userName: [
    { required: true, message: '请输入账号名', trigger: 'blur' },
    { min: 3,  message: '至少大于等于3位', trigger: 'blur' },
  ],
password:[
{ required: true, message: '请输入密码', trigger: 'blur' },
    { min: 3,  message: '至少大于等于6位', trigger: 'blur' },

]})
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
