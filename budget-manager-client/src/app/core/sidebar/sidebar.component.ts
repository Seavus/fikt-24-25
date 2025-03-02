import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ButtonComponent } from '../../shared/components/button/button.component';
import { Router, RouterModule } from '@angular/router';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-sidebar',
  imports: [
    ButtonComponent,
    RouterModule,
    MatSidenavModule,
    MatIconModule,
    MatToolbarModule,
    CommonModule,
  ],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.scss',
})
export class SidebarComponent {
  constructor(private readonly router: Router) {}
  @Input() isMenuOpened: boolean = false;
  @Output() toggleMenu = new EventEmitter<void>();

  logout() {
    this.router.navigate(['/login']);
  }

  toggle() {
    this.isMenuOpened = !this.isMenuOpened;

    const pElements = document.querySelectorAll('.sidebar-nav li p');
    const aElements = document.querySelectorAll('.sidebar-nav li a');

    pElements.forEach((pElement: Element) => {
      if (pElement instanceof HTMLElement) {
        pElement.style.display = this.isMenuOpened ? 'inline' : 'none';
      }
    });
    aElements.forEach((aElement: Element) => {
      if (aElement instanceof HTMLElement) {
        aElement.style.gap = '10px';
      }
    });

    this.toggleMenu.emit();
  }
}
