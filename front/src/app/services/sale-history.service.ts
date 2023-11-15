import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { ApiService } from './api.service';
import { Sale } from '../models/sale.model';

@Injectable({
  providedIn: 'root'
})
export class SaleHistoryService {

  http = inject(HttpClient)
  api = inject(ApiService)


  getSales(){
    const url = 'sale/history'
    return this.api.get<Sale[]>(url)
  }

  getSale(id: string) {
    const url = `sale/${id}`
    return this.api.get<Sale>(url)
  }
  
}
