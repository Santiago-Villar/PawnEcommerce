import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/auth/login/login.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './pages/home/home.component';
import { CartComponent } from './pages/cart/cart.component';

const routes: Routes = [
{ path: '', component: HomeComponent },
{ path: 'login', component: LoginComponent },
{ path: 'register', component: RegisterComponent },
{path: 'cart', component: CartComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes), CommonModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
