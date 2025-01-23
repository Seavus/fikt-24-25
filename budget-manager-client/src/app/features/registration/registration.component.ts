import { Component } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ButtonComponent } from '../../shared/components/button/button.component';
import { InputComponent } from '../../shared/components/input/input.component';


@Component({
  selector: 'app-registration',
  imports: [ButtonComponent,InputComponent, ReactiveFormsModule],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.scss'
})
export class RegistrationComponent {
  registerForm;
    constructor(private readonly fb: FormBuilder) {
      this.registerForm = this.fb.group({
      fullName: ['', Validators.required],
      email: ['', Validators.required, Validators.email],
    password: ['', Validators.required, Validators.minLength(8)]
  });
  }
    

  onSubmit(): void {
    console.log('submitted form',
       this.registerForm.value,
       this.registerForm.invalid
      );
  }
}
