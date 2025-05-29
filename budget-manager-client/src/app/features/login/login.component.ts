import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';

import { ButtonComponent } from '../../shared/components/button/button.component';
import { Component, inject, DestroyRef } from '@angular/core';
import { InputComponent } from '../../shared/components/input/input.component';
import { MatButtonModule } from '@angular/material/button';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { SnackbarService } from '../../core/services/snackbar.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { UserService } from '../../core/services/user.service';
import { UserStateService } from '../../core/services/user-state.service';

@Component({
  selector: 'app-login',
  imports: [
    InputComponent,
    MatButtonModule,
    ReactiveFormsModule,
    ButtonComponent,
    RouterModule,
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
  standalone: true,
})
export class LoginComponent {
  loginForm: FormGroup;
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);
  private readonly snackbarService = inject(SnackbarService);
  private readonly destroyRef = inject(DestroyRef);
  private readonly userService = inject(UserService);
  private readonly userStateService = inject(UserStateService);

  constructor(private readonly fb: FormBuilder) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
  }

  onLogin() {
    if (this.loginForm.valid) {
      const { email, password } = this.loginForm.getRawValue();
      this.authService
        .login({ email, password })
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe({
          next: (response) => {
            const userId = response.id;
            this.userService.getUserById(userId).subscribe({
              next: (userData) => {
                this.userStateService.setBalance(userData.balance);
                this.snackbarService.showSnackbar(
                  'Login successful!',
                  'success'
                );
                this.router.navigate(['']);
              },
              error: (error) => {
                this.snackbarService.showSnackbar(
                  'Failed to load user data',
                  'error'
                );
                console.error(error);
              },
            });
          },
          error: (err) => {
            this.snackbarService.showSnackbar(
              `Login failed: ${err.message}`,
              'error'
            );
          },
        });
    }
  }
}
