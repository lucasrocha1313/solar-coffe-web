<template>
  <div>
    <h1 id="customers-title">Manage Customers</h1>
    <hr />
    <div class="customer-actions">
      <solar-button @button:click="showNewCustomerModal">
        Add Customer
      </solar-button>
    </div>
    <table id="customers" class="table">
      <tr>
        <th>Customer</th>
        <th>Address</th>
        <th>State</th>
        <th>Since</th>
        <th>Delete</th>
      </tr>
      <tr v-for="customer in customers" :key="customer.id">
        <td>
          {{ customer.firstName + " " + customer.lastName }}
        </td>
        <td>
          {{
            customer.primaryAddress.addressLine1 +
            " " +
            customer.primaryAddress.addressLine2
          }}
        </td>
        <td>
          {{ customer.primaryAddress.state }}
        </td>
        <td>
          {{ humanizeDate(customer.createdOn) }}
        </td>
        <td>
          <i
            class="lni lni-cross-circle customer-delete"
            @click="deleteCustomer(customer.id)"
          ></i>
        </td>
      </tr>
    </table>
    <new-customer-modal
      v-if="isCustomerModalVisible"
      @close="closeModal"
      @save:customer="saveNewCustomer"
    />
  </div>
</template>

<script>
import { defineComponent } from "vue";
import SolarButton from "@/components/SolarButton";
import CustomerService from "@/services/CustomerService";
import NewCustomerModal from "@/components/modals/NewCustomerModal";
import DateMixin from "@/mixins/DateMixin";

const customerService = new CustomerService();
export default defineComponent({
  name: "CustomersView",
  components: { NewCustomerModal, SolarButton },
  mixins: [DateMixin],
  methods: {
    showNewCustomerModal() {
      this.isCustomerModalVisible = true;
    },
    closeModal() {
      this.isCustomerModalVisible = false;
    },
    async saveNewCustomer(newCustomer) {
      await customerService.addCustomer(newCustomer);
      this.isCustomerModalVisible = false;
      await this.initialize();
    },
    async deleteCustomer(id) {
      await customerService.deleteCustomer(id);
      this.initialize();
    },
    async initialize() {
      this.customers = await customerService.getCustomers();
    },
  },
  async created() {
    await this.initialize();
  },
  data() {
    return {
      isCustomerModalVisible: false,
      customers: [],
    };
  },
});
</script>

<style scoped lang="scss">
@import "@/scss/global.scss";
.customer-actions {
  display: flex;
  margin-bottom: 0.8rem;
  div {
    margin-right: 0.8rem;
  }
}

.customer-delete {
  cursor: pointer;
  font-weight: bold;
  font-size: 1.2rem;
  color: $solar-red;
}
</style>
