import { Component, OnInit, inject } from '@angular/core';
import { Product } from 'src/app/models/product.model';
import { ProductsService } from 'src/app/services/products.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './item-list-container.component.html',
  styleUrls: ['./item-list-container.component.css']
})
export class ProductListContainerComponent implements OnInit {

  products : Product[] = [];
  isLoading : boolean = false;

  productsService = inject(ProductsService)

  ngOnInit(): void {
    this.productsService.getProducts().subscribe((data : Product[]) => {
      this.products = data;
    });
  }

}
