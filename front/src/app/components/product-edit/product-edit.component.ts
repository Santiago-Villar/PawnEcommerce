import { Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Brand, Category, Color, Product } from 'src/app/models/product.model';
import { ProductsService } from 'src/app/services/products.service';

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['../admin-product-detail/admin-product-detail.component.css']
})
export class ProductEditComponent implements OnInit {
  product: Product | null = null;
  colors: Color[]  = [];
  brands: Brand[] = [];
  categories: Category[] = [];
  updatedProduct: Product | null = null;

  constructor(
    private route: ActivatedRoute,
    private productsService: ProductsService,
    private router: Router
  ) {}

  toastrService = inject(ToastrService)


  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const id = params['id'];
      this.loadProduct(id);
    });
    this.loadBrands();
    this.loadCategories();
    this.loadColors();
  }

  loadProduct(id: string): void {
    this.productsService.getProduct(id).subscribe((data: Product) => {
      this.product = data;
      this.updatedProduct = data;
    });
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

  goBack(): void {
    this.router.navigate(['/admin/products']);
  }

  compareProductObj(obj1: any, obj2: any): boolean {
    return obj1 && obj2 ? obj1.id === obj2.id : obj1 === obj2;
  }

  onCheckboxChange(event: any) {
    const isChecked = event.target.checked;
    if (this.updatedProduct) this.updatedProduct.isExcludedFromPromotions = isChecked;
  }


  saveChanges(formProduct: any): void {
    const colorIds = formProduct.colors.map((c : Color) => c.id);
    const { id, name, description, price, brand, category, stock, isExcludedFromPromotions } = formProduct;
    const updatedProduct = { name, description, price, brandId: brand.id, categoryId: category.id, colors: colorIds, stock, isExcludedFromPromotions }
    this.productsService.updateProduct(id, updatedProduct)
    .subscribe({
      next: () => {
        this.toastrService.success("Producto actualizado correctamente!", '', {
          progressBar: true,
          timeOut: 2000,
         });
      },
      error: () =>{
        this.toastrService.error("Ocurrio un error al actualizar el Producto", '', {
          progressBar: true,
          timeOut: 2000,
         });
      },

      complete: () => {
        this.router.navigate([`/admin/products/${id}`]);
      },
    })
  }

}
