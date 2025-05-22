import { Injectable, signal } from '@angular/core';
import { Observable, catchError, tap, throwError } from 'rxjs';

import { HttpClient } from '@angular/common/http';

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

export const currentUserSignal = signal<LoginResponse['id'] | null>(null);

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly apiUrl = 'api/account/token';

  constructor(private readonly http: HttpClient) {}

  get user$() {
    return currentUserSignal;
  }

  login(credentials: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(this.apiUrl, credentials).pipe(
      tap((response) => {
        if (response.accessToken) {
          localStorage.setItem('token', response.accessToken);
          currentUserSignal.set(response.id);
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
    currentUserSignal.set(null);
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }
}
