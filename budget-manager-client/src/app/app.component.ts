import { Component, inject } from '@angular/core';

import { AuthService } from './services/auth.service';
import { CommonModule } from '@angular/common';
import { MatSidenavModule } from '@angular/material/sidenav';
import { PopupData } from './shared/components/dynamic-popup/popup-data.model';
import { RouterOutlet } from '@angular/router';
import { SidebarComponent } from './core/sidebar/sidebar.component';

@Component({
  selector: 'app-root',
  imports: [SidebarComponent, CommonModule, MatSidenavModule, RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  private readonly authService = inject(AuthService);
  title = 'budget-manager-client';
  isMenuOpened = false;
  isPopupOpen: boolean = false;
  popupTitle: string = 'Dynamic Popup Title';
  popupData: PopupData = new PopupData('Hello! This is a dynamic popup.');

  get isLoggedIn(): boolean {
    return this.authService.isAuthenticated();
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
