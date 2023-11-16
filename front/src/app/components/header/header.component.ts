import { Component, OnInit, inject } from '@angular/core';
import { Router } from '@angular/router';
import { ADMIN_ROLE, USER_ROLE } from 'src/app/constants/roles';
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
  router = inject(Router);
  profileService = inject(ProfileService);
  
  isLoading: boolean = false;

  ngOnInit() {
    this.isLoading = true;
    this.profileService.getProfile().subscribe({
      next: ((data : User) => {
        this.isLoading = false;
        if(data.isAdmin) {
          this.profileService.setRole(ADMIN_ROLE);
        }
        if(data.isUser) {
          this.profileService.setRole(USER_ROLE)
        }
      }),
      error: (err: any) => {
        this.isLoading = false;
        this.profileService.removeRole(ADMIN_ROLE)
        this.profileService.setRole(USER_ROLE);
        this.authService.logout();
      }
    });
  }

  goBack(): void {
    this.router.navigate(['/']);
  }

  logout(): void {
    this.authService.logout();
    this.profileService.removeRole(ADMIN_ROLE)
    this.profileService.setRole(USER_ROLE);
    this.router.navigate(['/']);
  }
}
