import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { ProductListContainerComponent } from './components/itemList/item-list-container.component';
import { CommonModule } from '@angular/common';

const routes: Routes = [
{ path: '', component: ProductListContainerComponent },
{ path: 'login', component: LoginComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes), CommonModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
