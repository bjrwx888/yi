<template>
    <div class="login-wrapper">
        <h1>{{configStore.name}}-注册</h1>
        <div class="login-form">
            <div class="username form-item">
                <span>登录账号</span>
                <input type="text" class="input-item" v-model="registerForm.userName">
            </div>
            <div class="username form-item">
                <span>手机号</span>
                <input style="width: 70%;" type="text" class="input-item" v-model="registerForm.phone">
                <button v-if="!isSendCaptcha" style="width: 30%;background-color: #C14949;" class="login-btn" @click="captcha" >验证码</button>
                <button v-else style="width: 30%;background-color:#F0F2F5;" class="login-btn"  >已发送</button>
            </div>
            <div class="username form-item"  v-show="isSendCaptcha">
                <span>手机短信验证码</span>
                <input type="text" class="input-item" v-model="registerForm.code">
            </div>
            <div class="password form-item">
                <span>密码</span>
                <input type="password" class="input-item" v-model="registerForm.password">
            </div>
            <RouterLink to="/login"  > 已有账号，前往登录</RouterLink>
            <button class="login-btn" @click="register">注册</button>
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
import { reactive ,ref} from 'vue';
import { useRouter, useRoute } from 'vue-router';
import useUserStore from '@/stores/user.js'
import useConfigStore from "@/stores/config";
const  configStore= useConfigStore();
const userStore = useUserStore();
const router = useRouter();
const route = useRoute();
const registerForm = reactive({
    userName: "",
    password: "",
    uuid: "",
    code: ""
})

const isSendCaptcha=ref(false)

//验证码
const captcha=async()=>{
    isSendCaptcha.value=true;
}

const register = async () => {
    const response = await userStore.register(registerForm).catch((e) => {
        registerForm.userName = "";
        registerForm.password = "";
    });

    //成功
    if (response!=undefined) {
        ElMessage({
            message: `恭喜！${registerForm.userName}，注册成功！请登录！`,
            type: 'success',
        })

        const redirect = route.query?.redirect ?? '/login'
        router.push(redirect)

    }

}
</script>
<style src="@/assets/styles/login.scss" scoped></style>
