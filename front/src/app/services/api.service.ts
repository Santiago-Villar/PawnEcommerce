import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';
import { API_URL } from 'src/config';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private baseURL = API_URL;

  constructor(
    private httpClient: HttpClient,
    private authService: AuthService
  ) { }

  private getHeaders(): HttpHeaders {
    let headers = new HttpHeaders();

    const token = this.authService.getToken();
    if (token) {
      headers = headers.set('Authorization', `Bearer ${token}`);
    }

    return headers;
  }

  get<T>(endpoint: string): Observable<T> {
    return this.httpClient.get<T>(`${this.baseURL}/${endpoint}`, { headers: this.getHeaders() });
  }

  delete<T>(endpoint: string): Observable<T> {
    return this.httpClient.delete<T>(`${this.baseURL}/${endpoint}`, { headers: this.getHeaders() });
  }

  put<T>(endpoint: string, data: any): Observable<T> {
    return this.httpClient.put<T>(`${this.baseURL}/${endpoint}`, data, { headers: this.getHeaders() });
  }

  post<T>(endpoint: string, data: any): Observable<T> {
    return this.httpClient.post<T>(`${this.baseURL}/${endpoint}`, data, { headers: this.getHeaders() });
  }
}
