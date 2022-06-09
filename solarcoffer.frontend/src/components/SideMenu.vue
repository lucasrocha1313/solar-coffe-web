<template>
  <div class="side-menu-container">
    <router-link to="/">
      <img
        id="img-logo"
        alt="Solar coffe logo"
        src="../assets/images/solar_coffee_logo.png"
      />
    </router-link>
    <h1>Management Portal</h1>
    <solar-button
      id="menu-inventory"
      is-full-width
      @button:click="goToRoute('/inventory')"
    >
      Inventory
    </solar-button>
    <solar-button
      id="menu-customers"
      is-full-width
      @button:click="goToRoute('/customers')"
    >
      Manage Customers
    </solar-button>
    <solar-button
      id="menu-invoice"
      is-full-width
      @button:click="goToRoute('/invoice/new')"
    >
      New Invoice
    </solar-button>
    <solar-button
      id="menu-orders"
      is-full-width
      @button:click="goToRoute('/orders')"
    >
      Orders
    </solar-button>
  </div>
</template>

<script lang="ts">
import SolarButton from "./SolarButton.vue";
import { defineComponent } from "vue";
export default defineComponent({
  components: { SolarButton },
  name: "SideMenu",
  methods: {
    async goToRoute(route: string) {
      await this.$router.push(route);
    },
  },
});
</script>

<style scoped lang="scss">
@import "@/scss/global.scss";
.side-menu-container {
  background-color: #fcfcfc;
  height: 100vh;
  width: $menu-width;
  display: flex;
  flex-direction: column;
  padding: 0.8rem;
  box-shadow: 0 1px 1px rgba(0, 0, 0, 0.12), 0 1px 1px rgba(0, 0, 0, 0.24);
  box-sizing: border-box;
}

#img-logo {
  width: 100%;
}

.h1 {
  font-size: 1.2rem;
  margin: 1rem 0;
}
</style>
