// src/app/features/transaction-page/transaction-page.component.ts
import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { WrapperTableComponent } from '../../shared/components/wrapper-table/wrapper-table.component';
import { Transaction } from '../../shared/components/wrapper-table/wrapper-table.interface';
import { DynamicPopupComponent } from '../../shared/components/dynamic-popup/dynamic-popup.component';
import { TransactionService } from '../../core/services/transaction.service';
import { TransactionFormComponent } from '../transaction-form/transaction-form.component';

@Component({
  selector: 'app-transaction-page',
  standalone: true,
  imports: [CommonModule, WrapperTableComponent, DynamicPopupComponent, TransactionFormComponent],
  templateUrl: './transaction-page.component.html',
  styleUrls: ['./transaction-page.component.scss']
})
export class TransactionPageComponent {
  transactions: Transaction[] = [];

  constructor(private transactionService: TransactionService, public dialog: MatDialog) {}

  ngOnInit() {
    this.loadTransactions();
  }

  loadTransactions() {
    this.transactionService.getTransactions().subscribe((transactions) => {
      this.transactions = transactions;
    });
  }

  openDialog() {
    const dialogRef = this.dialog.open(DynamicPopupComponent);

    dialogRef.afterClosed().subscribe((newTransaction: Transaction) => {
      if (newTransaction) {
        this.transactionService.addTransaction(newTransaction).subscribe(() => {
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
}

onFormCancel() {
  this.showForm = false;
}
}
