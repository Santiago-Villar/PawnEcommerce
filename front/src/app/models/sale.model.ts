import { Product } from "./product.model";

export interface Sale {
  id: string;
  products: SaleProduct[];
  price: number;
  promotionName: string;
  date: Date;
  paymentMethod: string;
}

export interface SaleProduct { 
  product : Product;
  productId : string;
}