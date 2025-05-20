import { Component, DestroyRef } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import {
  RegisterUserRequest,
  RegisterUserResponse,
} from '../../core/interfaces/register-user.model';
import { Router, RouterModule } from '@angular/router';

import { ButtonComponent } from '../../shared/components/button/button.component';
import { HttpErrorResponse } from '@angular/common/http';
import { InputComponent } from '../../shared/components/input/input.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { RegisterService } from '../../core/services/register.service';
import { SnackbarComponent } from '../../shared/components/snackbar/snackbar.component';
import { currentUserSignal } from '../../services/auth.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-registration',
  imports: [ButtonComponent, InputComponent, ReactiveFormsModule, RouterModule],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.scss',
})
export class RegistrationComponent {
  registerForm;

  constructor(
    private readonly fb: FormBuilder,
    private readonly registerService: RegisterService,
    private readonly snackBar: MatSnackBar,
    private readonly router: Router,
    private readonly destroyRef: DestroyRef
  ) {
    this.registerForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      balance: ['', [Validators.required, Validators.min(1)]],
    });
  }

  onSubmit(): void {
    if (this.registerForm.invalid) {
      return;
    }

    const request = this.registerForm.getRawValue() as RegisterUserRequest;

    this.registerService
      .register(request)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (response: RegisterUserResponse) => {
          currentUserSignal.set(response.id);

          this.snackBar.openFromComponent(SnackbarComponent, {
            data: {
              message: 'Registration successful!',
              type: 'success',
            },
            duration: 3000,
          });
          console.log('User registered successfully', response);
          this.router.navigate(['']);
        },
        error: (error: HttpErrorResponse) => {
          this.snackBar.openFromComponent(SnackbarComponent, {
            data: {
              message: 'Registration failed. Please try again.',
              type: 'error',
            },
            duration: 3000,
          });
          console.error('Registration error', error);
        },
      });
  }
}
