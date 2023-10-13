import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { Token } from '../models/token.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  http = inject(HttpClient);

  constructor() { }

  login(email: string, password: string): Observable<Token>{
    const API_URL = 'https://localhost:7228/api/session/login';
    return this.http.post<Token>(API_URL, { email: email, password: password }).pipe(
      tap((token: Token) => this.saveToken(token)),
    );
  }

  getToken(): string | null {
    return localStorage.getItem('userToken');
  }

  logout(): void {
    localStorage.removeItem('userToken');
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  private saveToken(token: Token): void{
    localStorage.setItem('userToken', token.token);
  }
}
