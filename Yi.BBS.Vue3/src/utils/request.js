
import axios from 'axios';
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
    headers: {
        'Authorization': 'Bearer ' + ""
    },
})
// 请求拦截器
myaxios.interceptors.request.use(function (config) {
    return config;
}, function (error) {
    return Promise.reject(error);
});

// 响应拦截器
myaxios.interceptors.response.use(function (response) {

    return response.data;
}, function (error) {
    return Promise.reject(error);
});
export default myaxios