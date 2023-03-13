import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'

import './assets/main.css'
import * as ElementPlusIconsVue from '@element-plus/icons-vue'
import directive from './directive' // directive


const app = createApp(App)
for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
    app.component(key, component)
  }

app.use(createPinia())
directive(app);
app.use(router)
app.mount('#app')
