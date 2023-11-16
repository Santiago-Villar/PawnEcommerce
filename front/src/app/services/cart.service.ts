import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { BehaviorSubject, Observable, catchError, throwError } from 'rxjs';
import { API_URL } from 'src/config';
import { Discount } from '../models/discount.model';
import { Product } from '../models/product.model';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  api = inject(ApiService);
  
  getCart(): Product[] {
    const savedCart = localStorage.getItem('cart');
    if (savedCart) {
      const products : Product[] = JSON.parse(savedCart);
      return products;
    }
    return [];
  }

  addProduct(product: Product): void {
    const newCart = this.getCart();
    if(newCart.some(p => p.id === product.id))
      return;
    
    product.quantity = 1;
    newCart.push(product);
    localStorage.setItem('cart', JSON.stringify(newCart));
  }

  updateCart(event : { "stock": number, "id": string }[]): void {
    const newCart = this.getCart();

    const filteredCart = newCart.filter(cartItem => {
      return event.some(eventItem => eventItem.id === cartItem.id);
    });

    const updatedCart = filteredCart.map(cartItem => {
      const matchingEventItem = event.find(eventItem => eventItem.id === cartItem.id);
      const newQuantity = Math.min(cartItem.quantity, matchingEventItem?.stock || 0);
  
      return { ...cartItem, quantity: newQuantity };
    });

    localStorage.setItem('cart', JSON.stringify(updatedCart));
  }

  removeProduct(index: number): void {
    const currentCart = this.getCart();
    if (index >= 0 && index < currentCart.length) {
      currentCart.splice(index, 1);
      localStorage.setItem('cart', JSON.stringify(currentCart));
    }
  }

  resetCart(): void {
    localStorage.removeItem('cart');
  }

  updateQuantity(eventData: { index: number, quantity: number }): void {
    const newCart = this.getCart();
    newCart[eventData.index].quantity += eventData.quantity;
    localStorage.setItem('cart', JSON.stringify(newCart));
  }

  createSale(products: number[], paymentMethod: string){
    const URI = "sale";
    return this.api.post(URI, { "productIds": products, "paymentMethod": paymentMethod});
  }

  getDiscount(products: number[], paymentMethod: string) : Observable<Discount> {
    const URI = "sale/discount";

    return this.api.post<Discount>(URI, {"productIds": products, "paymentMethod": paymentMethod }).pipe(
      catchError((error) => {
        return throwError(() => error);
      }));
  }
}
