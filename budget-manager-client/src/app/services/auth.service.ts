import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap, catchError, throwError } from 'rxjs';
import { Router } from '@angular/router';

interface LoginRequest {
  email: string;
  password: string;
}

interface LoginResponse {
  accessToken: string;
  user: {
    id: number;
    name: string;
    email: string;
  };
}

export const currentUserSignal = signal<LoginResponse['user'] | null>(null);

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = '/api/account/token';

  constructor(private http: HttpClient, private router: Router) {}

  get user$() {
    return currentUserSignal;
  }

  login(credentials: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(this.apiUrl, credentials).pipe(
      tap((response) => {
        if (response.accessToken) {
          localStorage.setItem('token', response.accessToken);
          currentUserSignal.set(response.user);
        } else {
          console.error('No token found in the response');
        }
      }),
      catchError((error) => {
        console.error('Login failed:', error);
        return throwError(
          () => new Error(error.error?.message || 'Login failed')
        );
      })
    );
  }

  logout(): void {
    localStorage.removeItem('token');
    currentUserSignal.set(null);
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem('token');
  }
}
