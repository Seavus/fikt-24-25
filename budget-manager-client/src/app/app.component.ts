import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TransactionComponent } from './transaction-component/transaction-component';

@Component({
  selector: 'app-root',
  imports: [TransactionComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'budget-manager-client';
}
