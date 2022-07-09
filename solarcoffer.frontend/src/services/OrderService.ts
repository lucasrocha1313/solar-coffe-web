import axios from "axios";

export class OrderService {
  API_URL = process.env.VUE_APP_API_URL;

  public async getOrders() {
    const result = await axios.get(`${this.API_URL}/order/`);
    return result.data;
  }

  public async markOrderComplete(id: number) {
    const result = await axios.patch(`${this.API_URL}/order/complete/${id}`);
    return result.data;
  }
}
