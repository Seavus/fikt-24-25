import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatSidenavModule } from '@angular/material/sidenav';
import { RouterOutlet } from '@angular/router';
import { SidebarComponent } from './core/sidebar/sidebar.component';
import { currentUserSignal } from './services/auth.service';
import { TransactionPageComponent } from './features/transaction-page/transaction-page.component';
import { PopupData } from './shared/components/dynamic-popup/popup-data.model';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    MatSidenavModule,
    RouterOutlet,
    SidebarComponent,
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'], 
})
export class AppComponent {
  title = 'budget-manager-client';

  isMenuOpened: boolean = false;
  isPopupOpen: boolean = false;
  popupTitle: string = 'Dynamic Popup Title';
  popupData: PopupData = new PopupData('Hello! This is a dynamic popup.');

  constructor() {}

  get isLoggedIn(): boolean {
    return currentUserSignal() !== null;
  }

  togglePopup() {
    this.isPopupOpen = !this.isPopupOpen;
  }

  toggleMenu() {
    if (window.innerWidth < 1024) {
      this.isMenuOpened = !this.isMenuOpened;
    }
  }
}
