import { Component, OnInit, inject } from '@angular/core';
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

  cartService = inject(CartService);

  products : Product[] = this.cartService.getCart();
  quantity : number[] = [];

  ngOnInit() {
    this.products  = this.cartService.getCart();
    if(this.quantity.length == 0) {
      this.quantity = this.products.map(p => p.quantity)
    }
  }

  handleUpdateQuantity(eventData: { index: number, quantity: number }) {
    this.cartService.updateQuantity(eventData);
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
