import { Component, Input } from '@angular/core';
import { Product } from 'src/app/models/product.model';


@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.css']
})
export class ItemComponent {
  @Input() product: Product = null!;
  defaultImageUrl : String = "https://montevista.greatheartsamerica.org/wp-content/uploads/sites/2/2016/11/default-placeholder.png";
}
