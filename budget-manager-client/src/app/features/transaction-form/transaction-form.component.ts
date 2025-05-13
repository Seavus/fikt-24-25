import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';

@Component({
  selector: 'app-transaction-form',
  imports: [ReactiveFormsModule, MatButtonModule, MatSelectModule,MatInputModule, MatFormFieldModule],
  templateUrl: './transaction-form.component.html',
  styleUrl: './transaction-form.component.scss'
})
export class TransactionFormComponent {
  @Input() initialData: any = null;
  @Output() formSubmit = new EventEmitter<any>();
  @Output() cancel = new EventEmitter<void>();

  form: FormGroup;
  transactionForm: any;

  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      category: ['', Validators.required],
      type: ['', Validators.required],
      amount: [0, [Validators.required, Validators.min(0.01)]],
      description: ['']
    });
  }

  ngOnChanges() {
    if (this.initialData) {
      this.form.patchValue(this.initialData);
    }
  }

 onSubmit() {
  if (this.transactionForm.valid) {
    this.formSubmit.emit(this.transactionForm.value);
    this.transactionForm.reset();
  }
}

onCancel() {
  this.cancel.emit();
}

}
