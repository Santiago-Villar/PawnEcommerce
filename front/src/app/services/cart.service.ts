import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { API_URL } from 'src/config';
import { Discount } from '../models/discount.model';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  http = inject(HttpClient);

  createSale(products: number[]){
    const BASE_URL = `${API_URL}/sale`;
    return this.http.post(BASE_URL, { productDtosId: products});
  }

  getDiscount(products: number[]) : Observable<Discount> {
    const BASE_URL = `${API_URL}/sale/discount`;

    return this.http.post<Discount>(BASE_URL, products).pipe(
      catchError((error) => {
        return throwError(() => error);
      }));
  }
}
