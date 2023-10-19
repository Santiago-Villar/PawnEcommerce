import { Component, Input, SimpleChanges, inject } from '@angular/core';
import { Router } from '@angular/router';
import { Product } from 'src/app/models/product.model';

@Component({
  selector: 'app-summary',
  templateUrl: './summary.component.html',
  styleUrls: ['./summary.component.css']
})
export class SummaryComponent {
  router = inject(Router)
  
  @Input() products : Product[] = [];
  @Input() quantity : number[] = [];

  discount: number = 0;

  goToHome() {
    this.router.navigate(['']);
  }

  getSubtotal() {
    return this.getTotal() - this.discount;
  }

  getTotal() {
    return this.products.reduce((total, product, index) => total + (product.price * this.quantity[index]), 0);
  }

}
