import { Component } from '@angular/core';
import { PRODUCTS } from './TEST_PRODUCTS';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent {
  total: number = 0;
  discount: number = 0;
  subtotal: number = 0;

  products = PRODUCTS;
  quantity = Array(this.products.length).fill(1);
  
  handleUpdateQuantity(eventData: { index: number, quantity: number }) {
    this.quantity[eventData.index] += eventData.quantity;
  }

  handleRemoveProducts(eventData: { index: number }) {
    this.products.splice(eventData.index, 1);
  }

}
