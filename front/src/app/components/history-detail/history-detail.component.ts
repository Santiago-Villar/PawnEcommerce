import { Component, Input, OnInit, inject } from '@angular/core';
import { SaleHistoryService } from 'src/app/services/sale-history.service';
import { ActivatedRoute } from '@angular/router';
import { SaleProduct } from 'src/app/models/sale.model';

@Component({
  selector: 'app-history-detail',
  templateUrl: './history-detail.component.html',
  styleUrls: ['./history-detail.component.css']
})
export class HistoryDetailComponent implements OnInit{
  id : string | null = "";
  saleProducts: SaleProduct[] = [];
  ocurrences: Map<string, number> = new Map<string, number>();

  saleHistoryService = inject(SaleHistoryService);
  route = inject(ActivatedRoute);

  ngOnInit(): void {

    this.id = this.route.snapshot.paramMap.get("id");

    this.saleHistoryService.getSale(this.id!).subscribe((data : any) => {
      this.saleProducts = this.getFilteredProducts(data.products);
    });

  }

  getFilteredProducts(sp : SaleProduct[]): SaleProduct[] {
    const uniqueProductIds = new Set<string>();

    return sp.filter(product => {
      const isUnique = !uniqueProductIds.has(product.productId);
      uniqueProductIds.add(product.productId);

      if(this.ocurrences.get(product.productId) == null) { 
        this.ocurrences.set(product.productId, 1);
      } else {
        this.ocurrences.set(product.productId, this.ocurrences.get(product.productId)! + 1);
      }

      return isUnique;
    });
  }
}
