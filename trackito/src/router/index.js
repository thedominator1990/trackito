import { createRouter, createWebHistory } from "vue-router";


//import StorePage from "../views/StorePage";
import aliment from "../views/Aliment"
import training from "../views/Training"


const router = createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: "/",
            component: () => import("../views/Training.vue")
        },
        {
            path: "/training",
            name: "training",
            component: () => import("../views/Training.vue")
        },
        {
            path: "/aliment",
            name: "aliment",
            component: () => import("../views/Aliment.vue")
        },
        
    ],
});

export { routes };

export default router;