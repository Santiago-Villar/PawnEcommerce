import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['../auth.component.css']
})
export class RegisterComponent {
  router = inject(Router)
  
  email: string = '';
  password: string = '';
  confirmPassword: string = '';
  address: string = '';
  isLoading: boolean = false;

  register() {}
   
  goToLogIn() {
    this.router.navigate(['/login']);
  }
}
