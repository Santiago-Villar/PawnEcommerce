import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/auth/login/login.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './pages/home/home.component';
import { CartComponent } from './components/cart/cart.component';
import { AdminProductsComponent } from './components/admin-products/admin-products.component';
import { AdminProductDetailComponent } from './components/admin-product-detail/admin-product-detail.component';
import { ProductEditComponent } from './components/product-edit/product-edit.component';
import { ProductCreateComponent } from './components/product-create/product-create.component';
import { ProfileComponent } from './components/profile/profile.component';
import { ProfileEditComponent } from './components/profile-edit/profile-edit.component';
import { SaleHistoryComponent } from './components/sale-history/sale-history.component';
import { HistoryDetailComponent } from './components/history-detail/history-detail.component';
import { authGuard } from './guards/auth.guard';
import { adminGuard } from './guards/admin.guard';
import { userGuard } from './guards/user.guard';


const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'cart', component: CartComponent, canActivate: [userGuard()]  },
  { path: 'profile', component: ProfileComponent, canActivate: [authGuard()] },
  { path: 'profile-edit', component: ProfileEditComponent, canActivate: [authGuard()] },
  { path: 'history', component: SaleHistoryComponent, canActivate: [userGuard(), authGuard()] },
  { path: 'history/:id', component: HistoryDetailComponent, canActivate: [userGuard(), authGuard()]  },
  { path: 'admin', canActivate:[adminGuard(), authGuard()], children: [
      { path: 'products', component: AdminProductsComponent },
      { path: 'products/create', component: ProductCreateComponent }, 
      { path: 'products/:id', component: AdminProductDetailComponent }, 
      { path: 'products/edit/:id', component: ProductEditComponent }, 
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes), CommonModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
