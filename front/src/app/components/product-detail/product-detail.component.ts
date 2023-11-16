import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Product } from 'src/app/models/product.model';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css']
})
export class ProductDetailComponent {
  @Input() product: Product | null = null;
  @Output() clearSelectedProduct = new EventEmitter();
  @Output() addToCart = new EventEmitter<Product>();

  defaultImageUrl : String = "https://montevista.greatheartsamerica.org/wp-content/uploads/sites/2/2016/11/default-placeholder.png";

  clearProduct() {
    this.clearSelectedProduct.emit();
  }

  addProductToCart() {
    this.addToCart.emit(this.product!);
    this.clearSelectedProduct.emit();
  }
}
