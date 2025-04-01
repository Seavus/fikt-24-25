import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { SidebarComponent } from './core/sidebar/sidebar.component';
import { PopupData } from './shared/components/dynamic-popup/popup-data.model';
import { MatSidenavModule } from '@angular/material/sidenav';
import { ActivatedRoute, Router, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [SidebarComponent, CommonModule, MatSidenavModule, RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'budget-manager-client';

  isPopupOpen: boolean = false;
  popupTitle: string = 'Dynamic Popup Title';
  popupData: PopupData = new PopupData('Hello! This is a dynamic popup.');
  isMenuOpened = false;
  showSidenav = true;

  constructor(
    private readonly router: Router,
    private readonly route: ActivatedRoute
  ) {
    this.router.events.subscribe(() => {
      this.showSidenav =
        this.route.firstChild?.snapshot.data['showSidenav'] !== false;
    });
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
