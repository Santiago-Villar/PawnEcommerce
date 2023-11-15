import { Component, OnInit, inject } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user.model';
import { AuthService } from 'src/app/services/auth.service';
import { ProfileService } from 'src/app/services/profile.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  authService = inject(AuthService);
  isLoading: boolean = false;
  isAdmin: boolean = false;
  router = inject(Router);
  profileService = inject(ProfileService);

  ngOnInit() {
    this.isLoading = true;
    this.profileService.getProfile().subscribe({
      next: ((data : User) => {
        this.isLoading = false;
        this.isAdmin = data.isAdmin;
      }),
      error: (err: any) => {
        this.isLoading = false;
        this.isAdmin = false;
        this.authService.logout();
      }
    });
  }

  goBack(): void {
    this.router.navigate(['/']);
  }

  logout(): void {
    this.authService.logout();
    this.isAdmin = false;
    this.router.navigate(['/']);
  }
}
