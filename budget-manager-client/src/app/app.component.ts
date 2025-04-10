import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatSidenavModule } from '@angular/material/sidenav';
import { RouterOutlet } from '@angular/router';
import { SidebarComponent } from './core/sidebar/sidebar.component';
import { currentUserSignal } from './services/auth.service';

@Component({
  selector: 'app-root',
  imports: [SidebarComponent, CommonModule, MatSidenavModule, RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'budget-manager-client';
  isMenuOpened = false;

  constructor() {}

  isPopupOpen: boolean = false;
  popupTitle: string = 'Dynamic Popup Title';
  popupData: PopupData = new PopupData('Hello! This is a dynamic popup.');
  isMenuOpened = false;
  get isLoggedIn(): boolean {
    return currentUserSignal() !== null;
  }

  constructor() {}

  togglePopup() {
    this.isPopupOpen = !this.isPopupOpen;
  }

  toggleMenu() {
    if (window.innerWidth < 1024) {
      this.isMenuOpened = !this.isMenuOpened;
    }
  }
}
