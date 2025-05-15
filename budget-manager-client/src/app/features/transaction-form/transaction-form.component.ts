import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CategoryService } from '../../core/services/category.sevice';

@Component({
  selector: 'app-transaction-form',
  templateUrl: './transaction-form.component.html',
})
export class TransactionFormComponent implements OnInit {
  form!: FormGroup;
  categories: { id: number; name: string }[] = [];

  constructor(
    private fb: FormBuilder,
    private categoryService: CategoryService
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      category: [''],
      type: [''],
      amount: [0],
      description: [''],
    });

    this.loadCategories();
  }

  loadCategories() {
    this.categoryService.getCategories().subscribe({
      next: (res: { id: number; name: string; }[]) => {
        this.categories = res;
      },
      error: (err: any) => {
        console.error('Error loading categories', err);
      }
    });
  }
}


