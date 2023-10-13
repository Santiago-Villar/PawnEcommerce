import { Component, inject } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
  
export class LoginComponent {
  authService = inject(AuthService)
  toastrService = inject(ToastrService)

  email: string = '';
  password: string = '';
  isLoading: boolean = false;

  login(): void{
    this.isLoading = true;

    this.authService.login(this.email, this.password).subscribe({
      next: (token) => {
        this.isLoading = false;
      },
      error: (response: any) => {
        this.toastrService.error(response?.error?.message, '', {
          progressBar: true,
          timeOut: 2000,
        });
        this.isLoading = false;
      }
    });
  }
}
