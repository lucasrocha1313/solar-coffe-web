import { IShipment } from "@/types/Shipment";
import axios from "axios";

/**
 * Inventory Service
 * Provide UI buisiness logic associated with product Inventory
 */
export class InventoryService {
  API_URL = process.env.VUE_APP_API_URL;

  public async getInventory(): Promise<any> {
    const result = await axios.get(`${this.API_URL}/inventory`);
    return result.data;
  }

  public async updateInventoryQuantity(shipment: IShipment) {
    const result = await axios.patch(`${this.API_URL}/inventory`, shipment);
    return result.data;
  }
}
