import { Component, OnInit, inject } from '@angular/core';
import { Product } from 'src/app/models/product.model';
import { CartService } from 'src/app/services/cart.service';
import { ProductsService } from 'src/app/services/products.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './item-list-container.component.html',
  styleUrls: ['./item-list-container.component.css']
})
export class ProductListContainerComponent implements OnInit {
  products : Product[] = [];
  cartProducts : Product[] = [];
  isLoading : boolean = false;
  selectedProduct: Product | null = null;

  productsService = inject(ProductsService)
  cartService = inject(CartService);

  ngOnInit(): void {
    this.productsService.getProducts().subscribe((data : Product[]) => {
      this.products = data;
    });
  }

  selectProduct(product: any) {
    this.selectedProduct = product;
  }

  clearSelectedProduct() {
    this.selectedProduct = null;
  }

  addToCart(product : Product) {
    this.cartService.addProduct(product);
    if(!this.cartProducts.some(p => p.id === product.id)) {
      this.cartProducts.push(product);
    }
  }
}
