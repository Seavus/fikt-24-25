import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatSidenavModule } from '@angular/material/sidenav';
import { RouterOutlet } from '@angular/router';
import { SidebarComponent } from './core/sidebar/sidebar.component';
import { currentUserSignal } from './services/auth.service';
import { PopupData } from './shared/components/dynamic-popup/popup-data.model';

@Component({
  selector: 'app-root',
  imports: [SidebarComponent, CommonModule, MatSidenavModule, RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  title = 'budget-manager-client';
  isMenuOpened = false;
  userId: string | null = null;
  isPopupOpen: boolean = false;
  popupTitle: string = 'Dynamic Popup Title';
  popupData: PopupData = new PopupData('Hello! This is a dynamic popup.');

  ngOnInit(): void {
    this.userId = currentUserSignal();
  }
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
