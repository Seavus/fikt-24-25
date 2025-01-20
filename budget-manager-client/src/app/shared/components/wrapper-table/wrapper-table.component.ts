import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { Transaction } from './wrapper-table.interface';

@Component({
  selector: 'app-wrapper-table',
  standalone: true,
  imports: [CommonModule, MatTableModule],
  templateUrl: './wrapper-table.component.html',
  styleUrl: './wrapper-table.component.scss'
})
export class WrapperTableComponent {
  @Input() displayedColumns: string[] = [];
  @Input() dataSource: Transaction[] = [];
}
