import axios from "axios";
import { ICustomer } from "@/types/Customer";

export default class CustomerService {
  API_URL = process.env.VUE_APP_API_URL;

  public async getCustomers() {
    const result = await axios.get(`${this.API_URL}/customers/`);
    return result.data;
  }

  public async addCustomer(newCostumer: ICustomer) {
    const result = await axios.post(`${this.API_URL}/customers/`, newCostumer);
    return result.data;
  }

  public async deleteCustomer(customerId: number) {
    const result = await axios.delete(
      `${this.API_URL}/customers/${customerId}`
    );
    return result.data;
  }
}
