<template>
  <div>
    <h1 id="invoice-title">Create Invoice</h1>
    <hr />
    <div class="invoice-step" v-if="invoiceStep === 1">
      <h2>Step 1: Select Customer</h2>
      <div v-if="customers.length" class="invoice-step-detail">
        <label for="customer">Customer:</label>
        <select
          v-model="selectCustomerId"
          class="invoice-customers"
          id="customer"
        >
          <option disabled value="">Please select a Customer</option>
          <option v-for="c in customers" :value="c.id" :key="c.id">
            {{ c.firstName + " " + c.lastName }}
          </option>
        </select>
      </div>
    </div>
    <div class="invoice-step" v-if="invoiceStep === 2">
      <h2>Step 2: Create Order</h2>
      <div v-if="inventory.length" class="invoice-step-detail">
        <label for="product">Product:</label>
        <select
          v-model="newItem.product"
          class="invoice-line-item"
          id="product"
        >
          <option disabled value="">Please Select a Product</option>
          <option v-for="i in inventory" :value="i.product" :key="i.product.id">
            {{ i.product.name }}
          </option>
        </select>
        <label for="quantity">Quantity:</label>
        <input v-model="newItem.quantity" id="quantity" type="number" min="0" />
      </div>
      <div class="invoice-items-actions">
        <solar-button
          :disabled="!newItem.product || !newItem.quantity"
          @button:click="addLineItem"
        >
          Add New Line
        </solar-button>
        <solar-button
          :disabled="!lineItems.length"
          @button:click="finalizeOrder"
        >
          Finalize Order
        </solar-button>
      </div>
      <div class="invoice-orders-list" v-if="lineItems.length">
        <div class="running-total">
          <h3>Running Total:</h3>
          {{ currencyUSD(runningTotal) }}
        </div>
        <hr />
        <table class="table">
          <thead>
            <tr>
              <th>Product</th>
              <th>Description</th>
              <th>Qty.</th>
              <th>Price</th>
              <th>Subtotal</th>
            </tr>
          </thead>
          <tr
            v-for="lineItem in lineItems"
            :key="`index_${lineItem.product.id}`"
          >
            <td>{{ lineItem.product.name }}</td>
            <td>{{ lineItem.product.description }}</td>
            <td>{{ lineItem.quantity }}</td>
            <td>{{ lineItem.product.price }}</td>
            <td>
              {{ currencyUSD(lineItem.product.price * lineItem.quantity) }}
            </td>
          </tr>
        </table>
      </div>
    </div>
    <div class="invoice-step" v-if="invoiceStep === 3"></div>
    <hr />
    <div class="invoice-steps-actions">
      <solar-button @button:click="prev" :disabled="!canGoPrev"
        >Previous</solar-button
      >
      <solar-button @button:click="next" :disabled="!canGoNext"
        >Next</solar-button
      >
      <solar-button @button:click="startOver">Start Over</solar-button>
    </div>
  </div>
</template>

<script>
import CustomerService from "@/services/CustomerService";
import { InventoryService } from "@/services/InventoryService";
import SolarButton from "@/components/SolarButton.vue";
import CurrencyMixin from "@/mixins/CurrencyMixin";

const customerService = new CustomerService();
const inventoryService = new InventoryService();
export default {
  name: "CreateInvoiceView",
  components: { SolarButton },
  mixins: [CurrencyMixin],
  data() {
    return {
      invoiceStep: 1,
      invoice: {
        createdOn: new Date(),
        updatedOn: new Date(),
        customerId: 1,
        lineItems: [],
      },
      customers: [],
      selectCustomerId: 0,
      inventory: [],
      lineItems: [],
      newItem: {
        product: undefined,
        quantity: 0,
      },
    };
  },
  computed: {
    canGoNext: function () {
      return (
        (this.invoiceStep === 1 && this.selectCustomerId !== 0) ||
        (this.invoiceStep === 2 && this.lineItems.length > 0)
      );
    },
    canGoPrev: function () {
      return this.invoiceStep !== 1;
    },
    runningTotal: function () {
      return this.lineItems.reduce(
        (a, b) => a + b["product"]["price"] * b["quantity"],
        0
      );
    },
  },
  created() {
    this.initialize();
  },
  methods: {
    async initialize() {
      this.customers = await customerService.getCustomers();
      this.inventory = await inventoryService.getInventory();
    },
    prev() {
      if (this.invoiceStep === 1) {
        return;
      }
      this.invoiceStep -= 1;
    },
    next() {
      if (this.invoiceStep === 3) {
        return;
      }
      this.invoiceStep += 1;
    },
    addLineItem() {
      let newItem = {
        product: this.newItem.product,
        quantity: parseInt(this.newItem.quantity.toString()),
      };

      const lineItem = this.lineItems.find(
        (item) => item.product.id === newItem.product.id
      );
      if (lineItem) {
        lineItem.quantity += newItem.quantity;
      } else {
        this.lineItems.push(newItem);
      }

      this.newItem = { product: undefined, quantity: 0 };
    },
    finalizeOrder() {
      this.invoiceStep = 3;
    },
    startOver() {
      this.invoice = { customerId: 0, lineItems: [] };
      this.invoiceStep = 1;
      this.lineItems = [];
      this.selectCustomerId = 0;
    },
  },
};
</script>

<style scoped lang="scss">
@import "@/scss/global.scss";
.invoice-step-detail {
  margin: 1.2rem;
}

.invoice-steps-actions {
  display: flex;
  width: 100%;
}

.invoice-orders-list {
  margin-top: 1.2rem;
  padding: 0.8rem;

  .line-item {
    display: flex;
    border-bottom: 1px dashed #ccc;
    padding: 0.8rem;
  }

  .item-col {
    flex-grow: 1;
  }
}

.invoice-item-actions {
  display: flex;
}

.price-pre-tax {
  font-weight: bold;
}

.price-final {
  font-weight: bold;
  color: $solar-green;
}

.due {
  font-weight: bold;
}
.invoice-header {
  width: 100%;
  margin-bottom: 1.2rem;
}

.invoice-logo {
  padding-top: 1.4rem;
  text-align: center;

  img {
    width: 280px;
  }
}
</style>
