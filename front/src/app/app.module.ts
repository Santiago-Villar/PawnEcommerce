import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import {HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ToastrModule } from 'ngx-toastr';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/auth/login/login.component';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { CommonModule, NgClass } from '@angular/common';
import { ProductListContainerComponent } from './components/itemList/item-list-container.component';
import { ItemComponent } from './components/item/item.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { ShoppingBagComponent } from './components/cart/shopping-bag/shopping-bag.component';
import { SummaryComponent } from './components/cart/summary/summary.component';
import { CartComponent } from './components/cart/cart.component';
import { HeaderComponent } from './components/header/header.component';
import { HomeComponent } from './pages/home/home.component';
import { ProductDetailComponent } from './components/product-detail/product-detail.component';
import { AdminProductsComponent } from './components/admin-products/admin-products.component';
import { AdminProductDetailComponent } from './components/admin-product-detail/admin-product-detail.component';
import { ProductEditComponent } from './components/product-edit/product-edit.component';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { ProductCreateComponent } from './components/product-create/product-create.component';
import { MatDialogModule } from '@angular/material/dialog';
import { ConfirmDialogComponent } from './components/confirm-dialog/confirm-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    ShoppingBagComponent,
    SummaryComponent,
    CartComponent,
    ProductListContainerComponent,
    ItemComponent,
    RegisterComponent,
    HeaderComponent,
    HomeComponent,
    CartComponent,
    ProductDetailComponent,
    AdminProductsComponent,
    AdminProductDetailComponent,
    ProductEditComponent,
    ProductCreateComponent,
    ConfirmDialogComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatSelectModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDialogModule,
    ToastrModule.forRoot()
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
