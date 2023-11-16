import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { ApiService } from './api.service';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  http = inject(HttpClient)
  api = inject(ApiService)


  getUsers(){
    const url = 'user'
    return this.api.get<User[]>(url)
  }

  getUser(id : string){
    const url = `user/${id}`;
    return this.api.get<User>(url);
  }

  updateUser(id : string, user: any){
    const url = `user/${id}`;
    return this.api.put(url,user);
  }

  deleteUser(id : string){
    const url = `user/${id}`;
    return this.api.delete(url);
  }

  createUser(user: any){
    const url = `user`;
    return this.api.post<User>(url,user);
  }
  
}
