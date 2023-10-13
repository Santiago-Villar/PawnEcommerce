import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable, catchError, switchMap, tap, throwError } from 'rxjs';
import { Token } from '../models/token.model';
import { User } from '../models/user.model';

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
      catchError((error) => {
        return throwError(() => error);
      })
    );
  }

  register(user: User): Observable<Token> {
    const API_URL = 'https://localhost:7228/api/user';
  
    return this.http.post<Token>(API_URL, user).pipe(
      switchMap(() => this.login(user.email, user.password)),
      catchError((error) => {
        return throwError(() => error);
      }));
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
