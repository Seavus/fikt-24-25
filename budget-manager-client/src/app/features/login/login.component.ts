import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';

import { ButtonComponent } from '../../shared/components/button/button.component';
import { Component } from '@angular/core';
import { InputComponent } from '../../shared/components/input/input.component';
import { MatButtonModule } from '@angular/material/button';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [
    InputComponent,
    MatButtonModule,
    ReactiveFormsModule,
    ButtonComponent,
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
  constructor(private readonly fb: FormBuilder, private router: Router, private route: ActivatedRoute) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
  }

  onLogin() {
    if (this.loginForm.value) {
      console.log('Login attempt:', this.loginForm.getRawValue());

      localStorage.setItem('user', JSON.stringify({ username: 'testUser' }));
      const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
      this.router.navigate([returnUrl]);
    }
  }
}
