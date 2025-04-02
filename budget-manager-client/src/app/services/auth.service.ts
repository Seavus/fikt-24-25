import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap, catchError, throwError } from 'rxjs';

interface LoginRequest {
  email: string;
  password: string;
}

interface LoginResponse {
  token: string;
  user: {
    id: number;
    name: string;
    email: string;
  };
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'http://localhost:5230/api/account/token';

  private http = inject(HttpClient);

  login(credentials: LoginRequest): Observable<LoginResponse> { 
    return this.http.post<LoginResponse>(this.apiUrl, credentials).pipe(
      tap((response) => {
        if (response.token) {
          localStorage.setItem('token', response.token);
        }
      }),
      catchError((error) => {
        return throwError(() => new Error(error.error?.message || 'Login failed'));
      })
    );
  }

  logout(): void {
    localStorage.removeItem('token');
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem('token');
  }
}
