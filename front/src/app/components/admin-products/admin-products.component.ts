import { Component, HostListener, OnInit, inject } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Product } from 'src/app/models/product.model';
import { ProductsService } from 'src/app/services/products.service';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-admin-products',
  templateUrl: './admin-products.component.html',
  styleUrls: ['./admin-products.component.css']
})
export class AdminProductsComponent implements OnInit {
  products: Product[] = [];
  innerWidth: number = window.innerWidth;
  isSmallScreen : boolean = this.innerWidth < 900;
  isMobile : boolean = this.innerWidth < 650;
  isLoading: boolean = true;

  constructor(private router: Router, private dialog: MatDialog) {}

  productsService = inject(ProductsService)
  toastrService = inject(ToastrService)

  ngOnInit(): void {
    this.loadProducts();
  }

  get hasProducts(): boolean {
    return this.products.length > 0;
  }

  @HostListener('window:resize', ['$event'])
  onResize(event : any) {
    this.innerWidth = window.innerWidth;
    this.isSmallScreen = this.innerWidth < 900;
    this.isMobile = this.innerWidth < 650;
  }

  loadProducts(): void {
    this.productsService.getProducts().subscribe((data : Product[]) => {
      this.products = data.reverse();
      this.isLoading = false;
    }, (error) => this.isLoading = false);
  }

  seeProduct(id: string): void {
    this.router.navigate(['/admin/products', id]);
  }

  editProduct(id: string): void {
    this.router.navigate(['/admin/products/edit', id]);
  }

  createProduct(): void {
    this.router.navigate(['/admin/products/create']);
  }

  deleteProduct(id: string): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: { modelName: 'Producto' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.productsService.deleteProduct(id)
        .subscribe({
          next: () => {
            this.toastrService.success("Producto eliminado correctamente!", '', {
              progressBar: true,
              timeOut: 2000,
            });
            this.products  = this.products.filter((product : Product) => product.id !== id);
          },
          error: () => {
            this.toastrService.error("Ocurrió un error al eliminar el Producto", '', {
              progressBar: true,
              timeOut: 2000,
            });
          },
        });
      }
    });
  }
}
