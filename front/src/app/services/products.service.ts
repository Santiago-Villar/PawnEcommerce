import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Product } from '../models/product.model';
import { API_URL } from 'src/config';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  http = inject(HttpClient)

  getProducts(){
    const BASE_URL = `${API_URL}/product`;
    return this.http.get<Product[]>(BASE_URL);
  }

}
