<template>
  <solar-modal>
    <template v-slot:header> Add New Customer </template>
    <template v-slot:body>
      <ul class="new-customer">
        <li>
          <label for="firstName">First Name</label>
          <input type="text" id="firstName" v-model="customer.firstName" />
        </li>
        <li>
          <label for="lastName">Last Name</label>
          <input type="text" id="lastName" v-model="customer.lastName" />
        </li>
        <li>
          <label for="address1">Address Line 1</label>
          <input
            type="text"
            id="address1"
            v-model="customer.primaryAddress.addressLine1"
          />
        </li>
        <li>
          <label for="address2">Address Line 2</label>
          <input
            type="text"
            id="address2"
            v-model="customer.primaryAddress.addressLine2"
          />
        </li>
        <li>
          <label for="city">City</label>
          <input type="text" id="city" v-model="customer.primaryAddress.city" />
        </li>
        <li>
          <label for="state">State</label>
          <input
            type="text"
            id="state"
            v-model="customer.primaryAddress.state"
          />
        </li>
        <li>
          <label for="postalCode">Postal Code</label>
          <input
            type="text"
            id="postalCode"
            v-model="customer.primaryAddress.postalCode"
          />
        </li>
        <li>
          <label for="country">Country</label>
          <input
            type="text"
            id="country"
            v-model="customer.primaryAddress.country"
          />
        </li>
      </ul>
    </template>
    <template v-slot:footer>
      <solar-button type="button" @click="save" aria-label="save new costumer">
        Save New Costumer
      </solar-button>
      <solar-button type="button" @click="close" aria-label="close modal">
        Close
      </solar-button>
    </template>
  </solar-modal>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import SolarButton from "@/components/SolarButton.vue";
import SolarModal from "@/components/modals/SolarModal.vue";
export default defineComponent({
  name: "NewCustomerModal",
  components: { SolarButton, SolarModal },
  data() {
    return {
      customer: {
        primaryAddress: {},
        createdOn: new Date(),
        updatedOn: new Date(),
        firstname: "",
        lastname: "",
      },
    };
  },
  methods: {
    save() {
      this.$emit("save:customer", this.customer);
    },
    close() {
      this.$emit("close");
    },
  },
});
</script>

<style scoped lang="scss">
.new-customer {
  display: flex;
  flex-wrap: wrap;
  list-style: none;
  padding: 0;
  margin: 1rem;

  input {
    width: 80%;
    height: 1.8rem;
    margin: 0.8rem 2rem 0.8rem 0.8rem;
    font-size: 1.1rem;
    line-height: 1.3rem;
    padding: 0.2rem;
    color: #555;
  }

  label {
    font-weight: bold;
    margin: 0.8rem;
    display: block;
  }
}
</style>
