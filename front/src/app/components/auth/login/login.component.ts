import { Component, inject } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { ProfileService } from 'src/app/services/profile.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['../auth.component.css']
})
  
export class LoginComponent {
  authService = inject(AuthService)
  toastrService = inject(ToastrService)
  profileService = inject(ProfileService)
  router = inject(Router)

  email: string = '';
  password: string = '';
  isLoading: boolean = false;

  login(): void{
    this.isLoading = true;

    this.authService.login(this.email, this.password).subscribe({
      next: (token) => {
        this.isLoading = false;
        this.profileService.clearRoles();
        this.router.navigate(['/']);
      },
      error: (response: any) => {
        this.toastrService.error(response?.error?.message ?? "Unexpected Error", '', {
          progressBar: true,
          timeOut: 2000,
        });
        this.isLoading = false;
      }
    });
  }
}
