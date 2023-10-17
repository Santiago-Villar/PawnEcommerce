import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Product } from '../models/product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  http = inject(HttpClient)

  getProducts(){
    const API_URL = 'https://localhost:7228/api/product';
    return this.http.get<Product[]>(API_URL);
  }

}
