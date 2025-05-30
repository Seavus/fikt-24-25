import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { Transaction } from './wrapper-table.interface';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'app-wrapper-table',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatIcon],
  templateUrl: './wrapper-table.component.html',
  styleUrl: './wrapper-table.component.scss'
})
export class WrapperTableComponent {
  @Input() displayedColumns: string[] = [];
  @Input() dataSource: Transaction[] = [];

  @Output() edit = new EventEmitter<any>();
  @Output() delete = new EventEmitter<any>();
}
