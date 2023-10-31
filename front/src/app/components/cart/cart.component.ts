import { Component, inject } from '@angular/core';
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
    this.quantity = [...this.quantity];
  }
  
  handleRemoveProducts(eventData: { index: number }) {
    this.products = [...this.products.slice(0, eventData.index), ...this.products.slice(eventData.index + 1)];
    this.quantity = [...this.quantity.slice(0, eventData.index), ...this.quantity.slice(eventData.index + 1)];
  }

  resetProducts() {
    this.products = [];
  }

}
