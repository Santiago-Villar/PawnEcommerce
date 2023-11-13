import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { BehaviorSubject, Observable, catchError, throwError } from 'rxjs';
import { API_URL } from 'src/config';
import { Discount } from '../models/discount.model';
import { Product } from '../models/product.model';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  http = inject(HttpClient);

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

  createSale(products: number[]){
    const BASE_URL = `${API_URL}/sale`;
    return this.http.post(BASE_URL, { productDtosId: products});
  }

  getDiscount(products: number[]) : Observable<Discount> {
    const BASE_URL = `${API_URL}/sale/discount`;

    return this.http.post<Discount>(BASE_URL, {"productIds": products}).pipe(
      catchError((error) => {
        return throwError(() => error);
      }));
  }
}
