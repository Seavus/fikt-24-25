import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SidebarComponent } from './core/sidebar/sidebar.component';
import { MatSidenavModule } from '@angular/material/sidenav';

@Component({
  selector: 'app-root',
  imports: [CommonModule, RouterOutlet, SidebarComponent, MatSidenavModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'budget-manager-client';

  isMenuOpened = false;

  toggleMenu() {
    if (window.innerWidth < 1024) {
      this.isMenuOpened = !this.isMenuOpened;
    }
  }
}
