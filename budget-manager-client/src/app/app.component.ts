import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { SidebarComponent } from './core/sidebar/sidebar.component';
import { ButtonComponent } from './shared/components/button/button.component';
import { PopupData } from './shared/components/dynamic-popup/popup-data.model';


@Component({
  selector: 'app-root',
  imports: [SidebarComponent, CommonModule, ButtonComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'budget-manager-client';

  isPopupOpen: boolean = false;
  popupTitle: string = "Dynamic Popup Title";
  popupData: PopupData = new PopupData("Hello! This is a dynamic popup.");

  constructor() {
    console.log(this.popupData.message);
  }

  togglePopup() {
    this.isPopupOpen = !this.isPopupOpen;
  }
}

