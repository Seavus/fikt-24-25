import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ButtonComponent } from '../../shared/components/button/button.component';
import { Component } from '@angular/core';
import { InputComponent } from '../../shared/components/input/input.component';
import { RouterModule } from '@angular/router';
import { RegisterService } from '../../core/services/register.service';
import { HttpErrorResponse } from '@angular/common/http';
import {
  RegisterUserRequest,
  RegisterUserResponse,
} from '../../core/interfaces/register-user.model';
import { SnackbarComponent } from '../../shared/components/snackbar/snackbar.component';
import { MatSnackBar } from '@angular/material/snack-bar';

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
    private readonly snackBar: MatSnackBar
  ) {
    this.registerForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
    });
  }

  onSubmit(): void {
    if (this.registerForm.invalid) {
      return;
    }

    const request = this.registerForm.getRawValue() as RegisterUserRequest;

    this.registerService.register(request).subscribe({
      next: (response: RegisterUserResponse) => {
        localStorage.setItem('userId', response.id);

        this.snackBar.openFromComponent(SnackbarComponent, {
          data: {
            message: 'Registration successful!',
            type: 'success',
          },
          duration: 3000,
        });
        console.log('User registered successfully', response);
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
