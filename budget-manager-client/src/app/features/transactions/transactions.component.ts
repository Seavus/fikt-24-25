import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { WrapperTableComponent } from '../../shared/components/wrapper-table/wrapper-table.component';
import { MatTableModule } from '@angular/material/table';

@Component({
  selector: 'app-transactions',
  standalone: true,
  imports: [CommonModule, WrapperTableComponent, MatTableModule],
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.scss'
})
export class TransactionsComponent {
  displayedColumns: string[] = ['category', 'date', 'description', 'amount', 'actions'];


  dataSource = [
    { category: 'Food', date: '2024-12-01', description: 'Grocery shopping', amount: '50.0' },
    { category: 'Transport', date: '2024-12-02', description: 'Bus ticket', amount: '2.5' },
    { category: 'Entertainment', date: '2024-12-05', description: 'Movie ticket', amount: '12.0' }
  ];

  onEdit(row: any) {
    console.log('Edit:', row);
  
  }

  onDelete(row: any) {
    const confirmed = confirm('Дали сте сигурни дека сакате да ја избришете трансакцијата?');
    if (confirmed) {
      this.dataSource = this.dataSource.filter(t => t !== row);
    }
  }
}
