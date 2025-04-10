import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {
  UpdateUserResponse,
  UpdateUserRequest,
  UserDetailsResponse,
} from '../interfaces/user.interface';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = 'api/account';

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('token');
    if (!token) {
      throw new Error('No token found');
    }
    return new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
  }

  getUserById(id: string): Observable<UserDetailsResponse> {
    const headers = this.getAuthHeaders();
    return this.http.get<UserDetailsResponse>(`${this.apiUrl}/${id}`, {
      headers,
    });
  }

  updateUser(data: UpdateUserRequest): Observable<UpdateUserResponse> {
    const headers = this.getAuthHeaders();
    return this.http.put<UpdateUserResponse>(this.apiUrl, data, { headers });
  }
}
