import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import InventoryView from "../views/Inventory.vue";
import CustomersView from "../views/Customers.vue";
import OrdersView from "../views/Orders.vue";
import CreateInvoiceView from "../views/CreateInvoice.vue";

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
  {
    path: "/customers",
    name: "customers",
    component: CustomersView,
  },
  {
    path: "/orders",
    name: "orders",
    component: OrdersView,
  },
  {
    path: "/invoice/new",
    name: "create-invoice",
    component: CreateInvoiceView,
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
