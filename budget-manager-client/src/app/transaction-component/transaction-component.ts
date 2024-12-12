import { Component, ViewChild } from '@angular/core';
import {MatTable, MatTableModule} from '@angular/material/table';
import {MatButtonModule} from '@angular/material/button';

export interface TransactionElement {
  category: string;
  date: string;
  description: string;
  amount: string;
}
const ELEMENT_DATA: TransactionElement[] = [
  {category: 'food', date: '16.11.2019', description: 'cheese', amount: '100$'},
]
@Component({
  selector: 'app-transaction-component',
  imports: [MatButtonModule, MatTableModule],
  templateUrl: './transaction-component.html',
  styleUrl: './transaction-component.scss'
})
export class TransactionComponent {
  displayedColumns: string[] = ['category', 'date', 'description', 'amount'];
  dataSource = [...ELEMENT_DATA];
  

  @ViewChild(MatTable)
  table!: MatTable<TransactionElement>;

  addData() {
    const randomElementIndex = Math.floor(Math.random() * ELEMENT_DATA.length);
    this.dataSource.push(ELEMENT_DATA[randomElementIndex]);
    this.table.renderRows();
  }

  removeData() {
    this.dataSource.pop();
    this.table.renderRows();
  }
}
 
