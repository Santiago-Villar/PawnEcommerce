import { Component, OnInit, inject } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Brand, Category, Color, Product } from 'src/app/models/product.model';
import { ProductsService } from 'src/app/services/products.service';

@Component({
  selector: 'app-product-create',
  templateUrl: './product-create.component.html', 
  styleUrls: ['../admin-product-detail/admin-product-detail.component.css']
})
export class ProductCreateComponent implements OnInit {
  colors: Color[]  = [];
  brands: Brand[] = [];
  categories: Category[] = [];
  newProduct: any = {};

  constructor(
    private productsService: ProductsService,
    private router: Router
  ) {}

  toastrService = inject(ToastrService)

  ngOnInit(): void {
    this.loadBrands();
    this.loadCategories();
    this.loadColors();
  }

  loadBrands(): void {
    this.productsService.getBrands().subscribe((data: Brand[]) => {
      this.brands = data;
    });
  }

  loadCategories(): void {
    this.productsService.getCategories().subscribe((data: Category[]) => {
      this.categories = data;
    });
  }

  loadColors(): void {
    this.productsService.getColors().subscribe((data: Color[]) => {
      this.colors = data;
    });
  }

  goBack(): void {
    this.router.navigate(['/admin/products']);
  }

  compareProductObj(obj1: any, obj2: any): boolean {
    return obj1 && obj2 ? obj1.id === obj2.id : obj1 === obj2;
  }

  onCheckboxChange(event: any) {
    const isChecked = event.target.checked;
    if (this.newProduct) this.newProduct.isExcludedFromPromotions = isChecked;
  }


  createProduct(formProduct: any): void {
    const colorIds = formProduct.colors.map((c: Color) => c.id);
    const { name, description, price, brand, category, stock, isExcludedFromPromotions } = formProduct;
    const newProductData = { name, description, price, brandId: brand.id, categoryId: category.id, colors: colorIds, stock, isExcludedFromPromotions }

    this.productsService.createProduct(newProductData)
    .subscribe({
      next: (p : Product) => {
        this.toastrService.success("Producto creado correctamente!", '', {
          progressBar: true,
          timeOut: 2000,
        });
        this.router.navigate([`/admin/products/${p.id}`]);
      },
      error: () => {
        this.toastrService.error("Ocurrió un error al crear el Producto", '', {
          progressBar: true,
          timeOut: 2000,
        });
      },
    });
  }
}
