import { BehaviorSubject, Observable } from 'rxjs';
import { Injectable, inject } from '@angular/core';
import {
  UpdateUserRequest,
  UpdateUserResponse,
  UserDetailsResponse,
} from '../interfaces/user.interface';

import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = 'api/account';
  private readonly balanceSubject = new BehaviorSubject<number>(0);
  balance$ = this.balanceSubject.asObservable();

  getUserById(id: string): Observable<UserDetailsResponse> {
    return this.http.get<UserDetailsResponse>(`${this.apiUrl}/${id}`);
  }

  updateUser(data: UpdateUserRequest): Observable<UpdateUserResponse> {
    return this.http.put<UpdateUserResponse>(this.apiUrl, data);
  }

  setBalance(balance: number) {
    this.balanceSubject.next(balance);
    localStorage.setItem('balance', balance.toString());
  }

  clearBalance() {
    this.balanceSubject.next(0);
    localStorage.removeItem('balance');
  }
}
