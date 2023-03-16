import { createRouter, createWebHistory } from 'vue-router'
import Layout from '../layout/Index.vue'
import NotFound from '../views/error/404.vue'
import LoginLayout from '../layout/LoginLayout.vue'
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  scrollBehavior(to, from, savedPosition) {
    // 始终滚动到顶部
    return { top: 0 }
  },
  routes: [
{
  path: '/loginLayout',
      name: 'loginLayout',
      component: LoginLayout,
      redirect: '/login' ,
      children :[
        {
          name:'login',
          path: '/login',
          component: () => import('../views/Login.vue')
        },
        // {
        //   name:'register',
        //   path: '/register',
        //   component: () => import('../views/Register.vue')
        // },
      ]
    },

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
          path: '/article/:discussId/:articleId?',
          component: () => import('../views/Article.vue')
        },
        {
          name:'discuss',
          path: '/discuss/:plateId',
          component: () => import('../views/Discuss.vue')
        },
        {
          //artType：discuss主题、article文章
          //operType：create创建、update更新
          name:'editArt',
          path:'/editArt',
          component:()=>import('../views/EditArticle.vue')
        }
      ]
    },
    { path: '/:pathMatch(.*)*', name: 'NotFound', component: NotFound },
  ]
})

export default router
