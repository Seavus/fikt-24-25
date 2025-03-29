import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';

import { ButtonComponent } from '../../shared/components/button/button.component';
import { Component, inject } from '@angular/core';
import { InputComponent } from '../../shared/components/input/input.component';
import { MatButtonModule } from '@angular/material/button';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  imports: [
    InputComponent,
    MatButtonModule,
    ReactiveFormsModule,
    ButtonComponent,
    RouterModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
  standalone: true,
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  isRequired: boolean = true;
  loginForm: FormGroup;
  private authService = inject(AuthService);
  private router = inject(Router);
  
  constructor(private readonly fb: FormBuilder) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
  }

  onLogin() {
    if (this.loginForm.valid) {
      const { email, password } = this.loginForm.getRawValue();
      this.authService.login(email, password).subscribe({
        next: () => {
          console.log('Login successful');
          this.router.navigate(['/dashboard']); 
        },
        error: (err) => {
          console.error('Login failed:', err);
        },
      });
    }
  }
}
