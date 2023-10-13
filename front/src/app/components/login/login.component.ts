import { Component } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
  
export class LoginComponent {

  email: String = '';
  password: String = '';

  constructor() {}

  login(): void{
    console.log("login test")
  }
}
