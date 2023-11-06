import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  authService = inject(AuthService);
  // TODO: check if is admin
  isAdmin: boolean = true;
  constructor(
    private router: Router
  ) {}

  goBack(): void {
    this.router.navigate(['/']);
  }
}
