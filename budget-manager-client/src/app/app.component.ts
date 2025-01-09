import { Component } from '@angular/core';
import { SidebarComponent } from './core/sidebar/sidebar.component';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from './shared/components/button/button.component';
import { TransactionsComponent } from './features/transactions/transactions.component';



@Component({
  selector: 'app-root',
  imports: [ SidebarComponent, CommonModule, ButtonComponent, TransactionsComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'budget-manager-client';
}
