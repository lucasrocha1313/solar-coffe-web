<template>
  <div>
    <h1 id="orders-title">Sales Orders</h1>
    <hr />
    <table id="sales-orders" class="table" v-if="orders.length">
      <thead>
        <tr>
          <th>Customer Id</th>
          <th>Customer Id</th>
          <th>Order Total</th>
          <th>Order Status</th>
          <th>Mark Complete</th>
        </tr>
      </thead>
      <tr v-for="order in orders" :key="order.id">
        <td>{{ order.customer.id }}</td>
        <td>{{ order.id }}</td>
        <td>{{ currencyUSD(getTotal(order)) }}</td>
        <td :class="{ green: order.isPaid }">{{ getStatus(order.isPaid) }}</td>
        <td>
          <div
            v-if="!order.isPaid"
            class="lni lni-checkmark-circle order-complete green"
            @click="markComplete(order.id)"
          />
        </td>
      </tr>
    </table>
  </div>
</template>

<script>
import { OrderService } from "@/services/OrderService";
import CurrencyMixin from "@/mixins/CurrencyMixin";

const orderService = new OrderService();
export default {
  name: "OrdersView",
  mixins: [CurrencyMixin],
  data() {
    return {
      orders: [],
    };
  },
  methods: {
    async initialize() {
      this.orders = await orderService.getOrders();
    },
    getTotal(order) {
      return order.salesOrderItems.reduce(
        (a, b) => a + b["product"]["price"] * b["quantity"],
        0
      );
    },
    getStatus(isPaid) {
      return isPaid ? "Paid in full" : "Unpaid";
    },
    async markComplete(orderId) {
      await orderService.markOrderComplete(orderId);
      await this.initialize();
    },
  },
  async created() {
    await this.initialize();
  },
};
</script>

<style scoped lang="scss">
@import "@/scss/global.scss";

.green {
  font-weight: bold;
  color: $solar-green;
}
.order-complete {
  cursor: pointer;
  text-align: center;
}
.inventory {
  display: flex;
  margin-bottom: 0.8rem;
}
</style>
