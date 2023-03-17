<template>
  <h2>  登录-欢迎</h2>
    <el-input v-model="loginForm.userName" placeholder="用户名" />
    <el-input v-model="loginForm.password" placeholder="密码" />
    <el-button class="login-btn" type="primary" @click="login">登录</el-button>
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
<style scoped>
h2{
    text-align: center;
}
.el-input
{
margin:0rem 0 0.5rem 0;

}
.login-btn
{
    width: 100%;
}
</style>