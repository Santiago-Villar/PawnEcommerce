import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable, catchError, switchMap, tap, throwError } from 'rxjs';
import { Token } from '../models/token.model';
import { User } from '../models/user.model';
import { API_URL } from 'src/config';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  http = inject(HttpClient);

  login(email: string, password: string): Observable<Token>{
    const BASE_URL = `${API_URL}/session/login`;

    return this.http.post<Token>(BASE_URL, { email, password }).pipe(
      tap((token: Token) => this.saveToken(token)),
      catchError((error) => {
        return throwError(() => error);
      })
    );
  }

  register(user: { email: string, password: string, address: string }): Observable<Token> {
    const BASE_URL = `${API_URL}/user`;
  
    return this.http.post<Token>(BASE_URL , user).pipe(
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
