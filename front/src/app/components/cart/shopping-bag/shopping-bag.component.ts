import { Component } from '@angular/core';
import { Color } from '../../../models/product.model'
import { PRODUCTS } from './TEST_PRODUCTS';

@Component({
  selector: 'app-shopping-bag',
  templateUrl: './shopping-bag.component.html',
  styleUrls: ['./shopping-bag.component.css']
})
export class ShoppingBagComponent {
  defaultImageUrl : String = "https://montevista.greatheartsamerica.org/wp-content/uploads/sites/2/2016/11/default-placeholder.png";
  products = PRODUCTS;
  
  quantity: number[] = this.products.map(product => 1);
  total: number[] = this.products.map(product => product.price);

  updateTotal(i: number) {
    this.total[i] = Math.round(this.quantity[i] * this.products[i].price * 100) / 100;
  }

  addQuantity(i: number) { 
    if (this.quantity[i] < 9) {
      this.quantity[i]++;
      this.updateTotal(i);
    }
  }

  subtractQuantity(i: number) { 
    if (this.quantity[i] > 1) {
      this.quantity[i]--;
      this.updateTotal(i);
    }
  }

  removeProduct(i: number) { 
    this.products.splice(i, 1);
    this.quantity.splice(i, 1);
    this.total.splice(i, 1);
  }

  getColors(colors: Color[]) {
    return colors.map(col => col.name).join(', ');
  }
}
