import { createRouter, createWebHistory } from 'vue-router'
import Layout from '../layout/Index.vue'
import NotFound from '../views/NotFound.vue'
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'layout',
      component: Layout,
      redirect: '/index' ,
      children :[
        {
          name:'index',
          path: '/index',
          component: () => import('../views/Index.vue')
        },
        {
          name:'article',
          path: '/article',
          component: () => import('../views/Article.vue')
        },
        {
          name:'discuss',
          path: '/discuss',
          component: () => import('../views/Discuss.vue')
        },
      ]
    },
    { path: '/:pathMatch(.*)*', name: 'NotFound', component: NotFound },
  ]
})

export default router
