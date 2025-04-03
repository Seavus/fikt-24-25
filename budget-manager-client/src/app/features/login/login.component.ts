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
import { DynamicPopupComponent } from '../../shared/components/dynamic-popup/dynamic-popup.component';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';

@Component({
  selector: 'app-login',
  imports: [
    InputComponent,
    MatButtonModule,
    ReactiveFormsModule,
    ButtonComponent,
    RouterModule,
    MatDialogModule,
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
  standalone: true,
})
export class LoginComponent {
  loginForm: FormGroup;
  private authService = inject(AuthService);
  private router = inject(Router);
  private dialogService = inject(MatDialog);

  constructor(private readonly fb: FormBuilder) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
  }

  onLogin() {
    if (this.loginForm.valid) {
      const { email, password } = this.loginForm.getRawValue();
  
      this.authService.login({ email, password }).subscribe({
        next: () => {
          this.dialogService.open(DynamicPopupComponent, {
            data: { message: 'Login successful!', type: 'success' }
          });
          this.router.navigate(['/dashboard']);
        },
        error: (err) => {
          this.dialogService.open(DynamicPopupComponent, {
            data: { message: `Login failed: ${err.message}`, type: 'error' }
          });
        },
      });
    }
  }
}


