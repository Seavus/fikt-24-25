import { AfterViewInit, Component, DestroyRef, ViewChild } from '@angular/core';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import {
  Transaction,
  TransactionPayload,
} from '../../core/interfaces/transaction.model';

import { AddTransactionComponent } from './add-transaction/add-transaction.component';
import { ButtonComponent } from '../../shared/components/button/button.component';
import { CommonModule } from '@angular/common';
import { DynamicPopupWrapper } from '../../shared/components/dynamic-popup/dynamic-popup.component';
import { FormsModule } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { PopupData } from '../../shared/components/dynamic-popup/popup-data.model';
import { SnackbarService } from '../../core/services/snackbar.service';
import { TransactionService } from '../../core/services/transaction.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-transactions',
  standalone: true,
  imports: [
    ButtonComponent,
    FormsModule,
    CommonModule,
    MatPaginator,
    MatTableModule,
    MatIconModule,
  ],
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.scss',
})
export class TransactionsComponent implements AfterViewInit {
  displayedColumns: string[] = [
    'category',
    'description',
    'amount',
    'date',
    'transactionType',
    'actions',
  ];

  dataSource = new MatTableDataSource<Transaction>();
  totalItems: number = 0;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private readonly snackBar: SnackbarService,
    private readonly dialog: MatDialog,
    private readonly destroyRef: DestroyRef,
    private readonly transactionService: TransactionService
  ) {}

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;

    this.getTransactions();

    this.paginator.page
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(() => {
        this.getTransactions(
          this.paginator.pageIndex + 1,
          this.paginator.pageSize
        );
      });
  }

  getTransactions(PageIndex: number = 1, PageSize: number = 5): void {
    this.transactionService
      .getTransactions(PageIndex, PageSize)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (response) => {
          this.dataSource.data = response.items;
          this.totalItems = response.totalCount || response.items.length;
        },
        error: (err) => {
          this.snackBar.showSnackbar(
            `Error fetching transactions: ${err}`,
            'error'
          );
        },
      });
  }

  openTransactionDialog(): void {
    const dialogRef = this.dialog.open(AddTransactionComponent, {
      width: '400px',
    });

    dialogRef
      .afterClosed()
      .subscribe((result: TransactionPayload | undefined) => {
        if (result) {
          this.snackBar.showSnackbar('Transaction created.', 'success');
          this.getTransactions();
        }
      });
  }

  deleteTransaction(transaction: Transaction) {
    const data = new PopupData({
      title: 'Confirm Deletion',
      message: `Are you sure you want to delete the transaction: "${transaction.description}"?`,
      showInput: false,
      buttonText: 'Yes',
    });

    const dialogRef = this.dialog.open(DynamicPopupWrapper, {
      width: '300px',
      data,
      panelClass: 'custom-dialog',
    });

    dialogRef.afterClosed().subscribe((confirmed: boolean) => {
      if (confirmed) {
        this.transactionService
          .deleteTransaction(transaction.id as string)
          .pipe(takeUntilDestroyed(this.destroyRef))
          .subscribe(() => {
            this.snackBar.showSnackbar('Transaction deleted.', 'success');
            this.getTransactions();
          });
      }
    });
  }
}
