import {
  AfterViewInit,
  Component,
  OnDestroy,
  ViewChild,
} from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Subject, takeUntil } from 'rxjs';
import { AuthService } from '../../services/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { DynamicPopupWrapper } from '../../shared/components/dynamic-popup/dynamic-popup.component';
import { SnackbarComponent } from '../../shared/components/snackbar/snackbar.component';
import {
  Transaction,
  TransactionService,
} from '../../core/services/transaction.service';
import { PopupData } from '../../shared/components/dynamic-popup/popup-data.model';
import { Category } from '../../core/services/categories.service';

@Component({
  selector: 'app-transaction-form',
  templateUrl: './transaction-form.component.html',
  styleUrls: ['./transaction-form.component.scss'],
})
export class TransactionFormComponent implements OnDestroy, AfterViewInit {
  displayedColumns: string[] = ['categoryName', 'typeTransaction', 'amount', 'description', 'actions'];
  totalItems: number = 0;
  dataSource = new MatTableDataSource<Transaction>();

  private readonly destroy$ = new Subject<void>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  categoryService: any;

  constructor(
    private readonly authService: AuthService,
    private readonly snackBar: MatSnackBar,
    private readonly dialog: MatDialog,
    private readonly transactionService: TransactionService
  ) {}

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.getTransactions();

    this.paginator.page.pipe(takeUntil(this.destroy$)).subscribe(() => {
      this.getTransactions(this.paginator.pageIndex + 1, this.paginator.pageSize);
    });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
  categories: Category[] = [];

ngOnInit() {
  this.loadCategories();
}

loadCategories() {
  this.categoryService.getCategories().subscribe({
    next: (cats: Category[]) => this.categories = cats,
    error: (err: any) => console.error('Failed to load categories', err),
  });
}
  getTransactions(pageIndex: number = 1, pageSize: number = 5): void {
    if (!this.authService.getToken()) return;

    this.transactionService
      .getTransactions(pageIndex, pageSize)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (response) => {
          this.dataSource.data = response.items;
          this.totalItems = response.totalCount || response.items.length;
        },
        error: (err) => {
          console.error('Error fetching transactions:', err);
        },
      });
  }
  editTransaction(transaction: Transaction): void {
  const data = new PopupData({
    title: 'Edit Transaction',
    inputLabel: 'Category Name',
    inputFirst: transaction.typeTransaction,
    inputSecond: transaction.amount.toString(),
    inputThird: transaction.description,
    showInput: true,
    buttonText: 'Update',
  });

  const dialogRef = this.dialog.open(DynamicPopupWrapper, {
    width: '400px',
    data,
    panelClass: 'custom-dialog',
  });

  dialogRef.componentInstance.inputValue = transaction.categoryName;

  dialogRef.afterClosed().subscribe((result: {
    categoryName: string;
    type: string;
    amount: number;
    description: string;
  } | undefined) => {
    if (result && transaction.id) {
      const updatedTransaction: Transaction = {
        id: transaction.id,
        categoryName: result.categoryName,
        typeTransaction: result.type,
        amount: result.amount,
        description: result.description,
      };

      this.transactionService
        .updateTransaction(transaction.id, updatedTransaction)
        .pipe(takeUntil(this.destroy$))
        .subscribe({
          next: () => {
            this.snackBar.openFromComponent(SnackbarComponent, {
              data: { message: 'Transaction updated.', type: 'success' },
              duration: 3000,
            });
            this.getTransactions();
          },
          error: () => {
            this.snackBar.openFromComponent(SnackbarComponent, {
              data: { message: 'Failed to update transaction.', type: 'error' },
              duration: 3000,
            });
          },
        });
    }
  });
}
  addTransaction(): void {
    const data = new PopupData({
      title: 'Add New Transaction',
      inputLabel: 'Category Name',
      inputFirst: 'Type',
      inputSecond: 'Amount',
      inputThird: 'Description',
      showInput: true,
      buttonText: 'Add',
    });

    const dialogRef = this.dialog.open(DynamicPopupWrapper, {
      width: '400px',
      data,
      panelClass: 'custom-dialog',
    });

    dialogRef.afterClosed().subscribe((result: {
      categoryName: string;
      type: string;
      amount: number;
      description: string;
    } | undefined) => {
      if (result) {
        const transaction: Transaction = {
          categoryName: result.categoryName,
          typeTransaction: result.type,
          amount: result.amount,
          description: result.description,
        };

        this.transactionService
          .createTransaction(transaction)
          .pipe(takeUntil(this.destroy$))
          .subscribe({
            next: () => {
              this.snackBar.openFromComponent(SnackbarComponent, {
                data: { message: 'Transaction created.', type: 'success' },
                duration: 3000,
              });
              this.getTransactions();
            },
            error: () => {
              this.snackBar.openFromComponent(SnackbarComponent, {
                data: { message: 'Failed to create transaction.', type: 'error' },
                duration: 3000,
              });
            },
          });
      }
    });
  }

  deleteTransaction(transaction: Transaction): void {
    const data = new PopupData({
      title: 'Confirm Deletion',
      message: `Are you sure you want to delete the transaction for "${transaction.categoryName}"?`,
      showInput: false,
      buttonText: 'Yes',
    });

    const dialogRef = this.dialog.open(DynamicPopupWrapper, {
      width: '300px',
      data,
      panelClass: 'custom-dialog',
    });

    dialogRef.afterClosed().subscribe((confirmed: boolean) => {
      if (confirmed && transaction.id) {
        this.transactionService
          .deleteTransaction(transaction.id)
          .pipe(takeUntil(this.destroy$))
          .subscribe({
            next: (response) => {
              if (response.isSuccess) {
                this.snackBar.openFromComponent(SnackbarComponent, {
                  data: { message: 'Transaction deleted.', type: 'success' },
                  duration: 3000,
                });
                this.getTransactions();
              } else {
                this.snackBar.openFromComponent(SnackbarComponent, {
                  data: { message: 'Failed to delete transaction.', type: 'error' },
                  duration: 3000,
                });
              }
            },
            error: (error) => {
              console.error('Delete failed:', error);
              this.snackBar.openFromComponent(SnackbarComponent, {
                data: { message: 'Error deleting transaction.', type: 'error' },
                duration: 3000,
              });
            },
          });
      }
    });
  }
  
}
