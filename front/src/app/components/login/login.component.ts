import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
  
export class LoginComponent {
  http = inject(HttpClient);
  authService = inject(AuthService)

  email: string = '';
  password: string = '';
  isLoading: boolean = false;

  login(): void{
    this.isLoading = true;

    this.authService.login(this.email, this.password).subscribe({
      next: (token) => {
        console.log('token', token);
        this.isLoading = false;
      },
      error: (err: any) => {
        console.log('err', err);
        this.isLoading = false;
      }
    });
  }
}
