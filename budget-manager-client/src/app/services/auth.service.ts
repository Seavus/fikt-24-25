import { Observable, catchError, tap, throwError } from 'rxjs';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

interface LoginRequest {
  email: string;
  password: string;
}

interface LoginResponse {
  accessToken: string;
  id: string;
  name: string;
  email: string;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly apiUrl = 'api/account/token';

  constructor(private readonly http: HttpClient) {}

  login(credentials: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(this.apiUrl, credentials).pipe(
      tap((response) => {
        if (response.accessToken) {
          localStorage.setItem('token', response.accessToken);
          localStorage.setItem('userId', response.id);
        } else {
          console.error('No token found in the response');
        }
      }),
      catchError((error) => {
        return throwError(
          () => new Error(error.error?.message ?? 'Login failed')
        );
      })
    );
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('userId');
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }
}
