import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { ApiService } from './api.service';
import { User } from '../models/user.model';
import { ADMIN_ROLE, USER_ROLE } from '../constants/roles';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  http = inject(HttpClient)
  api = inject(ApiService)

  getProfile(){
    const url = 'user/profile'
    return this.api.get<User>(url)
  }

  updateProfile(id : string, profile : {email: string, address: string}) {
    const url = `user/${id}`
    return this.api.put<User>(url, profile);
  }

  setRole(role : string) {
    const jsonRoles = localStorage.getItem('roles');
    const roles = jsonRoles ? JSON.parse(jsonRoles) : [];

    if(!roles.includes(role)) {
      roles.push(role);
      localStorage.setItem('roles', JSON.stringify(roles));
    }
  }

  removeRole(role : string) {
    const jsonRoles = localStorage.getItem('roles');
    const roles : string[] = jsonRoles ? JSON.parse(jsonRoles) : [];

    localStorage.setItem('roles', JSON.stringify(roles.filter(r  => r != role)));
  }

  isAdmin() {
    const jsonRoles = localStorage.getItem('roles');
    const roles : string[] = jsonRoles ? JSON.parse(jsonRoles) : [];

    return roles.includes(ADMIN_ROLE);
  }

  isUser() {
    const jsonRoles = localStorage.getItem('roles');
    const roles : string[] = jsonRoles ? JSON.parse(jsonRoles) : [];

    return roles.includes(USER_ROLE);
  }

  clearRoles() {
    localStorage.removeItem('roles');
  }
}
