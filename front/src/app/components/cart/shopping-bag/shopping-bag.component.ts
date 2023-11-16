import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Color, Product } from '../../../models/product.model'

@Component({
  selector: 'app-shopping-bag',
  templateUrl: './shopping-bag.component.html',
  styleUrls: ['./shopping-bag.component.css']
})
export class ShoppingBagComponent {
  defaultImageUrl : String = "https://montevista.greatheartsamerica.org/wp-content/uploads/sites/2/2016/11/default-placeholder.png";

  @Output() updateQuantity = new EventEmitter<{ index: number, quantity: number }>();
  @Output() removeProduct = new EventEmitter<{ index: number }>();

  @Input() products : Product[] = [];
  @Input() quantity : number[] = [];

  total: number[] = [];

  ngOnInit() {
    this.total = this.products.map(product => product.price);
  }

  updateTotal(i: number) {
    this.total[i] = Math.round(this.quantity[i] * this.products[i].price * 100) / 100;
  }

  addQuantity(i: number) { 
    if (this.quantity[i] < 9) {
      this.updateQuantity.emit({ index: i, quantity: 1 });
      this.updateTotal(i);
    }
  }

  subtractQuantity(i: number) { 
    if (this.quantity[i] > 1) {
      this.updateQuantity.emit({ index: i, quantity: -1 });
      this.updateTotal(i);
    }
  }

  spliceProducts(i: number) { 
    this.removeProduct.emit({ index: i });
    this.total.splice(i, 1);
  }

  getColors(colors: Color[]) {
    if(colors){
      return colors.map(col => col.name).join(', ');
    }
    return []
  }
}
