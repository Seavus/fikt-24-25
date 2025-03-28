import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {
  RegisterUserRequest,
  RegisterUserResponse,
} from '../interfaces/register-user.model';

@Injectable({
  providedIn: 'root',
})
export class RegisterService {
  constructor(private readonly http: HttpClient) {}

  register(
    registerPayload: RegisterUserRequest
  ): Observable<RegisterUserResponse> {
    return this.http.post<RegisterUserResponse>(
      '/api/account',
      registerPayload
    );
  }
}
