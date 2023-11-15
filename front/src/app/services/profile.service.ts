import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { ApiService } from './api.service';
import { User } from '../models/user.model';

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
}
