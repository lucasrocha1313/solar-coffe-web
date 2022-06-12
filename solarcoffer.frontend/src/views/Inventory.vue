<template>
  <div class="inventory-container">
    <h1 id="invetory-title">Inventory Dashboard</h1>
    <hr />
    <div class="inventory-actions">
      <solar-button @button:click="showNewProductModel" id="addNewBtn">
        Add new item
      </solar-button>
      <solar-button @button:click="showShipmentModel" id="receiveShipmentBtn">
        Receive Shipment
      </solar-button>
    </div>
    <table id="inventory-table" class="table">
      <th>Item</th>
      <th>Quantity On-Hand</th>
      <th>Unit Price</th>
      <th>Taxable</th>
      <th>Delete</th>
      <tr v-for="item in inventory" :key="item.id">
        <td>
          {{ item.product.name }}
        </td>
        <td
          v-bind:class="`${applyColor(
            item.quantityOnHand,
            item.idealQuantity
          )}`"
        >
          {{ item.quantityOnHand }}
        </td>
        <td>
          {{ currencyUSD(item.product.price) }}
        </td>
        <td>
          <span v-if="item.product.isTaxable"> Yes </span>
          <span v-else> No </span>
        </td>
        <td>
          <i
            class="lni lni-cross-circle product-archive"
            @click="archiveProduct(item.product.id)"
          ></i>
        </td>
      </tr>
    </table>

    <new-product-modal
      v-if="isNewProductVisible"
      @save:product="saveNewProduct"
      @close="closeModal"
    />

    <shipment-modal
      v-if="isShipmentVisible"
      :inventory="inventory"
      @save:shipment="saveNewShipment"
      @close="closeModal"
    />
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import SolarButton from "@/components/SolarButton.vue";
import ShipmentModal from "@/components/modals/ShipmentModal.vue";
import NewProductModal from "@/components/modals/NewProductModal.vue";
import { IShipment } from "@/types/Shipment";
import { IProduct } from "@/types/Product";
import { InventoryService } from "@/services/InventoryService";
import { ProductService } from "@/services/ProductService";

const inventoryService = new InventoryService();
const productService = new ProductService();

export default defineComponent({
  name: "InventoryView",
  components: { SolarButton, ShipmentModal, NewProductModal },
  methods: {
    currencyUSD(value: number) {
      if (isNaN(value)) {
        return "-";
      }
      return `$${value.toFixed(2)}`;
    },
    showNewProductModel() {
      this.isNewProductVisible = true;
    },
    showShipmentModel() {
      this.isShipmentVisible = true;
    },
    async saveNewProduct(newProduct: IProduct) {
      await productService.save(newProduct);
      this.isNewProductVisible = false;
      await this.initialize();
    },
    async saveNewShipment(shipment: IShipment) {
      await inventoryService.updateInventoryQuantity(shipment);
      this.isShipmentVisible = false;
      await this.initialize();
    },
    closeModal() {
      this.isShipmentVisible = false;
      this.isNewProductVisible = false;
    },
    async initialize() {
      this.inventory = await inventoryService.getInventory();
    },
    applyColor(current: number, target: number) {
      if (current <= 0) return "red";
      if (Math.abs(current - target) > 8) return "yellow";
      return "green";
    },
    async archiveProduct(productId: number) {
      await productService.archive(productId);
      await this.initialize();
    },
  },
  data() {
    return {
      isShipmentVisible: false,
      isNewProductVisible: false,
      inventory: [],
    };
  },
  async created() {
    await this.initialize();
  },
});
</script>

<style scoped lang="scss">
@import "@/scss/global.scss";

.green {
  font-weight: bold;
  color: $solar-green;
}

.yellow {
  font-weight: bold;
  color: $solar-yellow;
}

.red {
  font-weight: bold;
  color: $solar-red;
}

.inventory-actions {
  display: flex;
  margin-bottom: 0.8rem;
}

.product-archive {
  cursor: pointer;
  font-weight: bold;
  font-size: 1.2rem;
  color: $solar-red;
}
</style>
