import { Component, Input } from '@angular/core';
import { EmailValidator, FormControl, FormGroupDirective, FormsModule, NgForm, Validators } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatError, MatFormField, MatFormFieldModule, MatHint, MatLabel } from '@angular/material/form-field';

@Component({
  selector: 'app-input',
  imports: [MatFormFieldModule, FormsModule, MatInputModule],
  templateUrl: './input.component.html',
  styleUrl: './input.component.scss',
  standalone: true,
})
export class InputComponent {
  @Input() label: string = ''; 
  @Input() placeholder: string = ''; 
  @Input() type: string = 'text'; 
  @Input() value: string = '';
  @Input() required: boolean = false;
}
