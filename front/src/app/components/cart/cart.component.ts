import { Component, OnInit, inject } from '@angular/core';
import { PRODUCTS } from './TEST_PRODUCTS';
import { CartService } from 'src/app/services/cart.service';
import { Product } from 'src/app/models/product.model';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  total: number = 0;
  discount: number = 0;
  subtotal: number = 0;

  products : Product[] = PRODUCTS;
  quantity = Array(this.products.length).fill(1);

  cartService = inject(CartService);

  ngOnInit() {
    this.products  = this.cartService.getCart();
  }

  handleUpdateQuantity(eventData: { index: number, quantity: number }) {
    this.quantity[eventData.index] += eventData.quantity;
    this.quantity = [...this.quantity];
  }
  
  handleRemoveProducts(eventData: { index: number }) {
    this.cartService.removeProduct(eventData.index);
    this.products = [...this.products.slice(0, eventData.index), ...this.products.slice(eventData.index + 1)];
    this.quantity = [...this.quantity.slice(0, eventData.index), ...this.quantity.slice(eventData.index + 1)];
  }

  resetProducts() {
    this.cartService.resetCart();
    this.products = [];
  }

}
