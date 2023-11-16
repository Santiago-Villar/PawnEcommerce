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
import { AdminUsersComponent } from './components/admin-users/admin-users.component';
import { AdminUserDetailComponent } from './components/admin-user-detail/admin-user-detail.component';
import { AdminUserCreateComponent } from './components/admin-user-create/admin-user-create.component';
import { AdminUserEditComponent } from './components/admin-user-edit/admin-user-edit.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { ProfileComponent } from './components/profile/profile.component';
import { ProfileEditComponent } from './components/profile-edit/profile-edit.component';
import { SaleHistoryComponent } from './components/sale-history/sale-history.component';
import { HistoryDetailComponent } from './components/history-detail/history-detail.component';


const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'cart', component: CartComponent },
  { path: 'profile', component: ProfileComponent },
  { path: 'profile-edit', component: ProfileEditComponent },
  { path: 'history', component: SaleHistoryComponent },
  { path: 'history/:id', component: HistoryDetailComponent },
  { path: 'admin', children: [
      { path: '', component: AdminDashboardComponent, pathMatch: 'full' },
      { path: 'products', component: AdminProductsComponent },
      { path: 'products/create', component: ProductCreateComponent }, 
      { path: 'products/:id', component: AdminProductDetailComponent }, 
      { path: 'products/edit/:id', component: ProductEditComponent }, 
      { path: 'users', component: AdminUsersComponent}, 
      { path: 'users/create', component: AdminUserCreateComponent},
      { path: 'users/edit/:id', component: AdminUserEditComponent },
      { path: 'users/:id', component: AdminUserDetailComponent},
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes), CommonModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
