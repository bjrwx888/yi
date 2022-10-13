import axios from 'axios'
// import store from '../store/index'
// import vm from '../main'
import JsonBig from 'json-bigint'
import { getToken } from '@/utils/auth'
import { useRouter } from "vue-router";
import useUserStore from '@/store/modules/user'
import { Notify } from 'vant';
// import VuetifyDialogPlugin from 'vuetify-dialog/nuxt/index';
export let isRelogin = { show: false };
const myaxios = axios.create({
        // baseURL:'/'// 
        baseURL: import.meta.env.VITE_APP_BASE_API, // /dev-apis
        timeout: 50000,
        headers: {
            'Authorization': 'Bearer ' + ""
        },
        //雪花id精度问题
        transformResponse: [ data => {
            const json = JsonBig({
              storeAsString: true
            })
            return json.parse(data)
          }],
    })
    // 请求拦截器
myaxios.interceptors.request.use(function(config) {
    const isToken = (config.headers || {}).isToken === false
    // 是否需要防止数据重复提交
    const isRepeatSubmit = (config.headers || {}).repeatSubmit === false
    if (getToken() && !isToken) {
        config.headers['Authorization'] = 'Bearer ' + getToken() // 让每个请求携带自定义token 请根据实际情况自行修改
      }
    // store.dispatch("openLoad");
    return config;
}, function(error) {
    return Promise.reject(error);
});

// 响应拦截器
myaxios.interceptors.response.use(async function(response) {
//成功
    const resp = response.data
  
    // store.dispatch("closeLoad");
    return resp;
}, async function(error) {
//未授权、失败
const resp = error.response.data


if (resp.code == undefined && resp.message == undefined) {
    Notify({ type: 'danger', message: '未知错误' });
} else if (resp.code == 401) {
    if (!isRelogin.show) {

     

      Notify({ type: 'warning', message: '登录过期' });
    //登出
    useUserStore().logOut().then(() => {
        location.href = '/';
      })

      isRelogin.show = false;
    }
    // const router = useRouter();
    // router.push({ path:"/login" });

    // const res = await vm.$dialog.error({
    //     text: `错误代码：${resp.code}，原因：${resp.message}<br>是否重新进行登录？`,
    //     title: '错误',
    //     actions: {
    //         'false': '取消',
    //         'true': '跳转'
    //     }
    // });
    // if (res) {
    //     vm.$router.push({ path: "/login" });
    // }

} else if (resp.code !== 200) {
    Notify({ type: 'danger', message: `错误代码：${resp.code}，原因：${resp.message}` });
    // vm.$dialog.notify.error(`错误代码：${resp.code}，原因：${resp.message}`, {
    //     position: "top-right",
    //     timeout: 5000,
    // });
}

    return Promise.reject(error);
});
export default myaxios