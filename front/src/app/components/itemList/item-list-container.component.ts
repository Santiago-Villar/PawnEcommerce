import { Component, OnInit, inject } from '@angular/core';
import { Brand, Category, Color, Product } from 'src/app/models/product.model';
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
  filteredProduct: any = {};
  colors: Color[]  = [];
  brands: Brand[] = [];
  categories: Category[] = [];

  productsService = inject(ProductsService)
  cartService = inject(CartService);

  ngOnInit(): void {
    this.productsService.getProducts().subscribe((data : Product[]) => {
      this.products = data;
    });
    this.loadBrands();
    this.loadCategories();
    this.loadColors();
  }

  loadBrands(): void {
    this.productsService.getBrands().subscribe((data: Brand[]) => {
      this.brands = data;
    })
  }

  loadCategories(): void {
    this.productsService.getCategories().subscribe((data: Category[]) => {
      this.categories = data;
    })
  }

  loadColors(): void {
    this.productsService.getColors().subscribe((data: Color[]) => {
      this.colors = data;
    })
  }

  selectProduct(product: any) {
    this.selectedProduct = product;
  }

  clearSelectedProduct() {
    this.selectedProduct = null;
  }

  compareProductObj(obj1: any, obj2: any): boolean {
    return obj1 && obj2 ? obj1.id === obj2.id : obj1 === obj2;
  }


  addToCart(product : Product) {
    this.cartService.addProduct(product);
    if(!this.cartProducts.some(p => p.id === product.id)) {
      this.cartProducts.push(product);
    }
  }

  resetFilters(){
    this.filteredProduct = {};
    console.log(this.filteredProduct);
    this.applyFilters();
  }


  applyFilters() {
    this.isLoading = true;
    const filter = {
      name: this.filteredProduct.search,
      brandId: this.filteredProduct.brand?.id,
      categoryId: this.filteredProduct.category?.id,
      minPrice: this.filteredProduct.minPrice,
      maxPrice: this.filteredProduct.maxPrice,
    }
    const params = '?' + Object.entries(filter)
    .filter(([key, value]) => value !== undefined && value !== null && value !== '')
    .map(([key, value]) => `${encodeURIComponent(key)}=${encodeURIComponent(value)}`)
    .join('&');    

    this.productsService.getFilteredProducts(params).subscribe(
      (filteredProducts: Product[]) => {
        this.products = filteredProducts;
        this.isLoading = false; 
      },
      error => {
        this.isLoading = false;
      }
    );
  }
}
