import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Brand, Category, Color, Product } from '../models/product.model';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  http = inject(HttpClient)
  api = inject(ApiService)


  getProducts(){
    const url = 'product'
    return this.api.get<Product[]>(url)
  }

  getProduct(id : string){
    const url = `product/${id}`;
    return this.api.get<Product>(url);
  }

  getBrands(){
    const url = `brand`;
    return this.api.get<Brand[]>(url);
  }

  getCategories(){
    const url = `category`;
    return this.api.get<Category[]>(url);
  }

  getColors(){
    const url = `color`;
    return this.api.get<Color[]>(url);
  }

  updateProduct(id : string, product: any){
    const url = `product/${id}`;
    return this.api.put(url,product);
  }

  deleteProduct(id : string){
    const url = `product/${id}`;
    return this.api.delete(url);
  }

  createProduct(product: any){
    const url = `product`;
    return this.api.post<Product>(url,product);
  }
  
}
