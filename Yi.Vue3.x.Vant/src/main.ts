import { createApp } from 'vue'
import './style.css'
import 'vant/es/image-preview/style';
import 'vant/es/toast/style';
import 'vant/es/dialog/style';
import 'vant/es/notify/style';
import router from './router'
import store from './store'
import './permission'
import App from './App.vue'

const app=createApp(App)
app.use(router)
app.use(store)
app.mount('#app');