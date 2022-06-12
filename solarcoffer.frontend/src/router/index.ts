import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import InventoryView from "../views/Inventory.vue";

const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    name: "home",
    component: InventoryView,
  },
  {
    path: "/inventory",
    name: "inventory",
    component: InventoryView,
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
