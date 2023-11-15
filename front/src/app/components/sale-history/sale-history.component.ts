import { Component, OnInit, inject } from '@angular/core';
import { Router } from '@angular/router';
import { Product } from 'src/app/models/product.model';
import { Sale } from 'src/app/models/sale.model';
import { SaleHistoryService } from 'src/app/services/sale-history.service';

@Component({
  selector: 'app-sale-history',
  templateUrl: './sale-history.component.html',
  styleUrls: ['./sale-history.component.css']
})
export class SaleHistoryComponent implements OnInit {
  sales: Sale[] = []
  isLoading: boolean = false

  saleHistoryService = inject(SaleHistoryService);
  router = inject(Router)
  
  ngOnInit(): void {
    this.saleHistoryService.getSales().subscribe((data : Sale[]) => {
      this.sales = data;
    });

  }

  navigateToDetails(id : string): void {
    this.router.navigate(['/history', id]);
  }

}

