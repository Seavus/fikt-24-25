import { Component } from '@angular/core';
import { InputComponent } from '../../shared/components/input/input.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  imports: [InputComponent, MatButtonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
  standalone: true,
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  isRequired: boolean = true;

  onLogin() {
    if (this.email && this.password) {
      
      console.log('Login attempt with email:', this.email, 'password:', this.password);
    }
  }
  
}
