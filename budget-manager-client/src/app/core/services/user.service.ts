import { HttpClient } from '@angular/common/http';
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

  getUserById(id: string): Observable<UserDetailsResponse> {
    return this.http.get<UserDetailsResponse>(`${this.apiUrl}/${id}`);
  }

  updateUser(data: UpdateUserRequest): Observable<UpdateUserResponse> {
    return this.http.put<UpdateUserResponse>(this.apiUrl, data);
  }

  getStoredBalance(): number {
    return Number(localStorage.getItem('balance'));
  }
}
