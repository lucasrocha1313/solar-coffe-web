import { IProduct } from "@/types/Product";

export interface IInvoice {
  customerId: number;
  lineItems: ILineItens;
  createdOn: Date;
  updatedOn: Date;
}

export interface ILineItens {
  product?: IProduct;
  quantity: number;
}
