import { Component, Input } from '@angular/core';
import { MatTableModule} from '@angular/material/table';
import {MatButtonModule} from '@angular/material/button';

@Component({
  selector: 'app-transaction-component',
  imports: [MatButtonModule, MatTableModule],
  templateUrl: './transaction-component.html',
  styleUrl: './transaction-component.scss'
})
export class TransactionComponent {
  @Input() displayedColumns: string[] = []; 
  @Input() dataSource: any[] = []; 

  constructor() {}
  }