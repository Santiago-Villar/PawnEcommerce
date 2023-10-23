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
import { CommonModule } from '@angular/common';
import { ProductListContainerComponent } from './components/itemList/item-list-container.component';
import { ItemComponent } from './components/item/item.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { HeaderComponent } from './components/header/header.component';
import { HomeComponent } from './pages/home/home.component';
import { CartComponent } from './pages/cart/cart.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ProductListContainerComponent,
    ItemComponent,
    RegisterComponent,
    HeaderComponent,
    HomeComponent,
    CartComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
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
