
import axios from 'axios';
import { getToken } from '@/utils/auth'
export let isRelogin = { show: false };
// import JsonBig from 'json-bigint'
const myaxios = axios.create({
    baseURL:import.meta.env.VITE_APP_BASEAPI,
    timeout: 50000,
    // transformResponse: [data => {
    //     try {
    //         const json = JsonBig({
    //             storeAsString: true
    //           })
    //         return json.parse(data)
    //       } catch (err) {
    //         // 如果转换失败，则包装为统一数据格式并返回
    //         return {
    //           data
    //         }
    //       }
    // }],
})
// 请求拦截器
myaxios.interceptors.request.use(function (config) {
    if (getToken()) {
        config.headers['Authorization'] = 'Bearer ' + getToken() // 让每个请求携带自定义token 请根据实际情况自行修改
      }
    return config;
}, function (error) {
    return Promise.reject(error);
});

// 响应拦截器
myaxios.interceptors.response.use(function (response) {

    return response;
}, function (error) {
const response=error.response.data;
//业务异常+应用异常，统一处理
 switch(response.code) 
 {

    case 403:  
    ElMessage.error(response.message)
    break;
    case 500:
    ElMessage.error(response.message)    
    break;
 }

    return Promise.reject(error);
});
export default myaxios