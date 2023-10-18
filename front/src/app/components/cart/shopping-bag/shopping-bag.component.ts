import { Component } from '@angular/core';

@Component({
  selector: 'app-shopping-bag',
  templateUrl: './shopping-bag.component.html',
  styleUrls: ['./shopping-bag.component.css']
})
export class ShoppingBagComponent {
  defaultImageUrl : String = "https://montevista.greatheartsamerica.org/wp-content/uploads/sites/2/2016/11/default-placeholder.png";
  quantity: number = 1;
  price: number = 19.99;
  total: number = this.price;

  updateTotal() {
    this.total = Math.round(this.quantity * this.price * 100) / 100;
  }

  addQuantity() { 
    if (this.quantity < 9) {
      this.quantity++;
      this.updateTotal();
    }
  }

  subtractQuantity() { 
    if (this.quantity > 1) {
      this.quantity--;
      this.updateTotal();
    }
  }
}
