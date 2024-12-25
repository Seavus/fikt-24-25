import { Component, Input } from '@angular/core';
import { MatTableModule} from '@angular/material/table';
import {MatButtonModule} from '@angular/material/button';

@Component({
  selector: 'app-transactions',
  imports: [MatButtonModule, MatTableModule],
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.scss'
})
export class TransactionsComponent {
  @Input() displayedColumns: string[] = []; 
  @Input() dataSource: any[] = []; 

  constructor() {}
}
