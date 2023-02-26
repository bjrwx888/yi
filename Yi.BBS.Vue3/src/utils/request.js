
import axios from 'axios';
const myaxios = axios.create({
    // baseURL:'/'// 
    // baseURL: process.env.VUE_APP_BASE_API, // /dev-apis
    timeout: 50000,
    // transformResponse: [data => {
    //     const json = JsonBig({
    //         storeAsString: true
    //     })
    //     return json.parse(data)
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

    return response;
}, function (error) {
    return Promise.reject(error);
});
export default myaxios