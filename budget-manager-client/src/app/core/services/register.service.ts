import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RegisterUserRequest,RegisterUserResponse } from '../../features/registration/registration.component';

@Injectable({
  providedIn: 'root',
})
export class RegisterService {

  constructor(private http: HttpClient) {}

  register(RegisterUserRequest: RegisterUserRequest): Observable<RegisterUserResponse> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.post<RegisterUserResponse>('/api/account', RegisterUserRequest);
  }
}
