<template>
    这个是登录页面
    <el-input v-model="loginForm.userName" placeholder="用户名" />
    <el-input v-model="loginForm.password" placeholder="密码" />
    <el-button type="primary" @click="login">登录</el-button>
</template>
<script setup>
import { reactive } from 'vue';
import { useRouter } from 'vue-router';
import useUserStore from '@/stores/user.js'
const  userStore=useUserStore();
const router=useRouter();
const loginForm=reactive({
    userName:"",
    password:"",
    uuid:"",
    code:""
})

const login=async ()=>{
const response= await userStore.login(loginForm);
if( response.code==undefined)
{
    router.push("/index")
}
}

</script>