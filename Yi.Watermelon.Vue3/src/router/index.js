import { createRouter, createWebHistory } from "vue-router";
import Layout from "../layout/Index.vue";
// import NotFound from "../views/error/404.vue";
const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    scrollBehavior(to, from, savedPosition) {
        // 始终滚动到顶部
        return { top: 0 };
    },
    routes: [
        {
            path: "/",
            name: "layout",
            component: Layout,
            redirect: "/index",
            children: [
                {
                    name: "index",
                    path: "/index",
                    components:{
                        default: ()=>import("../views/home/Index.vue"),
                        market: ()=>import("../views/market/Index.vue"),
                    } 
                },

            ]
        }

        // { path: "/:pathMatch(.*)*", name: "NotFound", component: NotFound },
    ],
});

export default router;