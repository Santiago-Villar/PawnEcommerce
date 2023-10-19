import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/models/user.model';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['../auth.component.css']
})
export class RegisterComponent {
  router = inject(Router)
  toastrService = inject(ToastrService)
  authService = inject(AuthService)

  user: User = { email: '', password: '', adress: '' };

  confirmPassword: string = '';
  isLoading: boolean = false;

  register() {
    if (this.user.password !== this.confirmPassword) { 
      this.toastrService.error('Passwords do not match', '', {
        progressBar: true,
        timeOut: 2000,
      });
      return;
    }

    this.isLoading = true;
    
    this.authService.register(this.user).subscribe({
      next: (token) => {
        this.isLoading = false;
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
   
  goToLogIn() {
    this.router.navigate(['/login']);
  }
}
