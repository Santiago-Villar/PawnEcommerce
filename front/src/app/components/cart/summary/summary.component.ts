import { Component, EventEmitter, Input, Output, SimpleChanges, inject } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Product } from 'src/app/models/product.model';
import { AuthService } from 'src/app/services/auth.service';
import { CartService } from 'src/app/services/cart.service';

@Component({
  selector: 'app-summary',
  templateUrl: './summary.component.html',
  styleUrls: ['./summary.component.css']
})
export class SummaryComponent {
  router = inject(Router)
  cartService = inject(CartService);
  toastrService = inject(ToastrService)

  @Input() products : Product[] = [];
  @Input() quantity : number[] = [];

  @Output() resetProducts = new EventEmitter();
  @Output() updateProducts = new EventEmitter();

  discount: number = 0;
  total: number = 0;
  subtotal: number = 0;
  isLoading: boolean = false;

  ngOnChanges() {
    this.setDiscount();
  }

  goToHome() {
    this.router.navigate(['']);
  }

  setDiscount() {
    if(this.products.length == 0){
      this.discount = 0;
      this.total = 0;
      this.subtotal = 0;
      return;
    }

    const productsId = Array.from(this.products).flatMap((product, i) => {
      const productId = Number.parseInt(product.id);
      const occurrences = this.quantity[i] || 0;
      return Array.from({ length: occurrences }, () => productId);
    });


    this.cartService.getDiscount(productsId, "Paganza").subscribe({
      next: (discount) => {
        this.discount = discount.promotionDiscount;
        this.total = discount.totalPrice;
        this.subtotal = discount.finalPrice;
      },
      error: (response: any) => {
        console.log(response)
        this.toastrService.error(response?.error?.message ?? "Unexpected Error", '', {
          progressBar: true,
          timeOut: 2000,
        });
        this.isLoading = false;
      }
    });
  }

  createSale(){
    this.isLoading = true;

    this.cartService.createSale(this.products.map(product => Number.parseInt(product.id)), "Paganza").subscribe({
      next: () => {
        this.isLoading = false;
        this.toastrService.success("Succesful sale!", '', {
          progressBar: true,
          timeOut: 2000,
        });

        this.resetProducts.emit();
        this.discount = 0;
      },
      error: (response: any) => {
        this.toastrService.error(response?.error?.message ?? "Please log in before checkout", '', {
          progressBar: true,
          timeOut: 3000,
        });

        const updatedCart : Product[] = response?.error?.updatedCart;
        if (updatedCart){
          const updatedCartStock = updatedCart.map(prod => ({ "stock": prod.stock, "id": prod.id }));
          this.updateProducts.emit(updatedCartStock);
        }

        this.isLoading = false;
      }
    });
  }

}
