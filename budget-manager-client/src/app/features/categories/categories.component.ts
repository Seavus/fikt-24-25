import { Component, OnDestroy, AfterViewInit, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject, takeUntil } from 'rxjs';
import { AuthService } from '../../services/auth.service';
import { InputComponent } from '../../shared/components/input/input.component';
import { ButtonComponent } from '../../shared/components/button/button.component';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SnackbarComponent } from '../../shared/components/snackbar/snackbar.component';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatIconModule } from '@angular/material/icon';

interface Category {
  name: string;
}

@Component({
  selector: 'app-categories',
  imports: [
    InputComponent,
    ButtonComponent,
    FormsModule,
    CommonModule,
    MatPaginator,
    MatTableModule,
    MatIconModule,
  ],
  templateUrl: './categories.component.html',
  styleUrl: './categories.component.scss',
})
export class CategoriesComponent implements OnDestroy, AfterViewInit {
  categoryName: string = '';
  displayedColumns: string[] = ['name', 'actions'];
  totalItems: number = 0;
  dataSource = new MatTableDataSource<Category>();

  private readonly destroy$ = new Subject<void>();
  constructor(
    private readonly http: HttpClient,
    private readonly authService: AuthService,
    private readonly snackBar: MatSnackBar
  ) {}

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;

    this.getCategories();

    this.paginator.page.pipe(takeUntil(this.destroy$)).subscribe(() => {
      this.getCategories(this.paginator.pageIndex, this.paginator.pageSize);
    });
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }
  createCategory() {
    if (!this.categoryName.trim()) {
      this.snackBar.openFromComponent(SnackbarComponent, {
        data: {
          message: 'Category name is required!',
          type: 'error',
        },
        duration: 3000,
      });
      return;
    }

    const token = this.authService.getToken();

    if (!token) {
      this.snackBar.openFromComponent(SnackbarComponent, {
        data: {
          message: 'You must be logged in to create a category.',
          type: 'error',
        },
        duration: 3000,
      });
      return;
    }

    const body = {
      name: this.categoryName,
    };

    this.http
      .post<{ categoryId: string }>('/api/categories', body)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (response) => {
          this.snackBar.openFromComponent(SnackbarComponent, {
            data: {
              message: 'Category created.',
              type: 'success',
            },
            duration: 3000,
          });
          this.categoryName = '';
        },
        error: (error) => {
          console.error(error);
          this.snackBar.openFromComponent(SnackbarComponent, {
            data: {
              message: 'Failed to create category. Please try again later.',
              type: 'error',
            },
            duration: 3000,
          });
        },
      });
  }

  getCategories(PageIndex: number = 1, PageSize: number = 5): void {
    const token = this.authService.getToken();
    if (!token) return;

    const params = {
      PageIndex,
      PageSize,
    };

    this.http
      .get<{
        items: Category[];
        totalCount: number;
      }>('/api/account/categories', { params })
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (response) => {
          this.dataSource.data = response.items;
          this.totalItems = response.totalCount || response.items.length;
        },
        error: (err) => {
          console.error('Error fetching categories:', err);
        },
      });
  }
}
