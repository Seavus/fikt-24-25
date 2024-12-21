import { Component } from '@angular/core';
import { TransactionComponent } from './shared/components/transaction-component/transaction-component';

@Component({
  selector: 'app-root',
  imports: [TransactionComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  displayedColumns: string[] = ['category', 'date', 'description', 'amount'];

  dataSource = [
    { category: 'Food', date: '2024-12-01', description: 'Grocery shopping', amount: 50.0 },
    { category: 'Transport', date: '2024-12-02', description: 'Bus ticket', amount: 2.5 },
    { category: 'Entertainment', date: '2024-12-05', description: 'Movie ticket', amount: 12.0 }
  ];
}
