import { Component, DestroyRef, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';

import { Category } from '../../../core/interfaces/category.model';
import { CategoryService } from '../../../core/services/categories.service';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { TransactionPayload } from '../../../core/interfaces/transaction.model';
import { TransactionService } from '../../../core/services/transaction.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-add-transaction',
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatSelectModule,
    MatInputModule,
    MatButtonModule,
    MatDialogModule,
  ],
  templateUrl: './add-transaction.component.html',
  styleUrl: './add-transaction.component.scss',
})
export class AddTransactionComponent implements OnInit {
  form: FormGroup;
  categories!: Category[];

  constructor(
    private readonly transactionService: TransactionService,
    private readonly categoriesService: CategoryService,
    private readonly fb: FormBuilder,
    private readonly dialogRef: MatDialogRef<AddTransactionComponent>,
    private readonly destroyRef: DestroyRef
  ) {
    this.form = this.fb.group({
      categoryId: ['', Validators.required],
      transactionType: [0, Validators.required],
      transactionDate: [new Date(), Validators.required],
      amount: [null, [Validators.required, Validators.min(0.01)]],
      description: [''],
    });
  }

  ngOnInit(): void {
    this.categoriesService
      .getCategories(1, 20)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((data) => {
        this.categories = data.items;
      });
  }

  submit(): void {
    if (this.form.valid) {
      this.transactionService
        .createTransaction(this.form.getRawValue())
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe(() => {
          this.dialogRef.close(this.form.value as TransactionPayload);
        });
    }
  }

  cancel(): void {
    this.dialogRef.close();
  }
}
