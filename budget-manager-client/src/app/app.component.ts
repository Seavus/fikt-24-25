import { Component } from '@angular/core';
import { SidebarComponent } from './core/sidebar/sidebar.component';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from './shared/components/button/button.component';
import { TransactionsComponent } from './features/transactions/transactions.component';
import { DynamicPopupComponent } from './shared/components/dynamic-popup/dynamic-popup.component';




@Component({
  selector: 'app-root',
  standalone: true,
  imports: [ SidebarComponent, CommonModule, ButtonComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'budget-manager-client';

  isPopupOpen: boolean = false;
  popupTitle: string = "Dynamic Popup Title";
  popupData: any = { message: "Hello! This is a dynamic popup." };

  openPopup() {
    this.isPopupOpen = true;
    this.popupData = { message: "New dynamic data loaded!" }; 
  }

  closePopup() {
    this.isPopupOpen = false;
  }
}
