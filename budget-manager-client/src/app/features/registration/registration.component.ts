import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ButtonComponent } from '../../shared/components/button/button.component';
import { Component } from '@angular/core';
import { InputComponent } from '../../shared/components/input/input.component';
import { RouterModule } from '@angular/router';
import { RegisterService } from '../../core/services/register.service'; 
import { HttpErrorResponse } from '@angular/common/http';

export interface RegisterUserRequest {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
}

export interface RegisterUserResponse {
  id: string;
}

@Component({
  selector: 'app-registration',
  imports: [ButtonComponent, InputComponent, ReactiveFormsModule,RouterModule],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.scss',
})
export class RegistrationComponent {
  registerForm;
 
  constructor(
    private readonly fb: FormBuilder,
    private readonly registerService: RegisterService
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
  
    const request: RegisterUserRequest = {
      firstName: this.registerForm.value.firstName ?? '',
      lastName: this.registerForm.value.lastName ?? '',
      email: this.registerForm.value.email ?? '',
      password: this.registerForm.value.password ?? '',
    };

    this.registerService.register(request).subscribe({
      next: (response: RegisterUserResponse) => {
        console.log('User registered successfully', response);

      },
      error: (error: HttpErrorResponse) => {
        console.error('Registration error', error);
      }
    });
  }
}