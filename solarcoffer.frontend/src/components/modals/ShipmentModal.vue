<template>
  <solar-modal>
    <template v-slot:header> Receive Shipment </template>
    <template v-slot:body>
      <label for="product">Product Received:</label>
      <select v-model="selectedroduct" class="shipment-items" id="product">
        <label>Please select one</label>
        <option v-for="item in inventory" :value="item" :key="item.product.id">
          {{ item.product.name }}
        </option>
      </select>
      <label for="qtyReceived">Quantity Received:</label>
      <input type="number" id="qtyReceived" v-model="qtyReceived" />
    </template>
    <template v-slot:footer>
      <solar-button
        type="button"
        @button:click="save"
        aria-label="save new shipment"
      >
        Save Received Shipment
      </solar-button>
      <solar-button
        type="button"
        @button:click="close"
        aria-label="Close modal"
      >
        Close
      </solar-button>
    </template>
  </solar-modal>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import SolarButton from "../SolarButton.vue";
import SolarModal from "@/components/modals/SolarModal.vue";
import { IProductInventory } from "@/types/Product";
import { IShipment } from "@/types/Shipment";

export default defineComponent({
  components: { SolarButton, SolarModal },
  name: "ShipmentModal",
  props: {
    inventory: {
      type: Array as () => IProductInventory[],
      required: true,
    },
  },
  methods: {
    close() {
      this.$emit("close");
    },
    save() {
      let shipment: IShipment = {
        productId: this.selectedroduct.id,
        adjustment: this.qtyReceived,
      };

      this.$emit("save:shipment", shipment);
    },
  },
  data() {
    return {
      selectedroduct: {
        id: 0,
        createdOn: new Date(),
        updatedOn: new Date(),
        name: "",
        description: "",
        price: 0,
        isTaxable: false,
        isArchived: false,
      },
      qtyReceived: 0,
    };
  },
});
</script>

<style scoped lang="scss"></style>
