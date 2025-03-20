import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';

import { ButtonComponent } from '../../shared/components/button/button.component';
import { Component } from '@angular/core';
import { InputComponent } from '../../shared/components/input/input.component';
import { MatButtonModule } from '@angular/material/button';
import { HttpClient } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [
    InputComponent,
    MatButtonModule,
    ReactiveFormsModule,
    ButtonComponent,
  ],
  providers: [],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
  standalone: true,
})
export class LoginComponent {
  loginForm: FormGroup;

  constructor(
    private readonly fb: FormBuilder,
    private readonly http: HttpClient,
    private readonly snackBar: MatSnackBar,
    private readonly router: Router
  ) {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }

  onLogin() {
    if (this.loginForm.valid) {
      const { username, password } = this.loginForm.value;

      this.http.post('https://dummyjson.com/auth/login', { username, password })
        .subscribe({
          next: (response: any) => {
            console.log('Login successful:', response);
            localStorage.setItem('userData', JSON.stringify(response));
            this.router.navigate(['/home']);
          },
          error: (error) => {
            console.error('Login failed:', error);
            this.snackBar.open('Login failed. Please check your credentials.', 'Close', {
              duration: 3000,
            });
          }
        });
    } else {
      this.snackBar.open('Please fill in all required fields correctly.', 'Close', {
        duration: 3000,
      });
    }
  }
}
