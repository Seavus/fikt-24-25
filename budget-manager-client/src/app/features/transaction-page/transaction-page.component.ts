import { Component, DestroyRef, inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { WrapperTableComponent } from '../../shared/components/wrapper-table/wrapper-table.component';
import { Transaction } from '../../shared/components/wrapper-table/wrapper-table.interface';
import { DynamicPopupComponent } from '../../shared/components/dynamic-popup/dynamic-popup.component';
import { TransactionService } from '../../core/services/transaction.service';
import { TransactionFormComponent } from '../transaction-form/transaction-form.component';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-transaction-page',
  standalone: true,
  imports: [
    CommonModule,
    WrapperTableComponent,
    DynamicPopupComponent,
    TransactionFormComponent
  ],
  templateUrl: './transaction-page.component.html',
  styleUrls: ['./transaction-page.component.scss']
})
export class TransactionPageComponent {
  transactions: Transaction[] = [];

  private destroyRef = inject(DestroyRef);

  constructor(
    private transactionService: TransactionService,
    public dialog: MatDialog
  ) {}

  ngOnInit() {
    this.loadTransactions();
  }

  loadTransactions() {
    this.transactionService.getTransactions()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((transactions) => {
        this.transactions = transactions;
      });
  }

  openDialog() {
    const dialogRef = this.dialog.open(DynamicPopupComponent);

    dialogRef.afterClosed()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((newTransaction: Transaction) => {
        if (newTransaction) {
          this.transactionService.addTransaction(newTransaction)
            .pipe(takeUntilDestroyed(this.destroyRef))
            .subscribe(() => {
              this.loadTransactions();
            });
        }
      });
  }

  showForm = false;

  onAddClick() {
    this.showForm = true;
  }

  onFormSubmit(transaction: Transaction) {
    console.log('Submitted:', transaction);
    this.showForm = false;
    this.transactionService.addTransaction(transaction)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(() => {
        this.loadTransactions();
      });
  }

  onFormCancel() {
    this.showForm = false;
  }
}

