<template>
  <h2>  登录-欢迎</h2>
    <el-input v-model="loginForm.userName" placeholder="用户名" />
    <el-input v-model="loginForm.password" placeholder="密码" show-password />
    <el-button class="login-btn" type="primary" @click="login" >登录</el-button>
    <br>
    <el-button class="login-btn" type="primary" @click="guestlogin" >游客临时登录</el-button>
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
const guestlogin=async ()=>{
    loginForm.userName="guest";
    loginForm.password="123456"
    await userStore.login(loginForm);
    router.push("/index")
}
const login=async ()=>{
const response= await userStore.login(loginForm);
console.log(response);
if( response.status==200)
{
    router.push("/index")
}
else
{
    alert("登录失败")
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
    margin-bottom: 0.5rem;
}
</style>