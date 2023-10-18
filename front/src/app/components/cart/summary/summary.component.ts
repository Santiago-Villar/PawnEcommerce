import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-summary',
  templateUrl: './summary.component.html',
  styleUrls: ['./summary.component.css']
})
export class SummaryComponent {
  router = inject(Router)

  total: number = 150;
  discount: number = 0;
  subtotal: number = this.total;
  
  goToHome() {
    this.router.navigate(['']);
  }
}
