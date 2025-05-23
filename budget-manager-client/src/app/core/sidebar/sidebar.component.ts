import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ButtonComponent } from '../../shared/components/button/button.component';
import { Router, RouterModule } from '@angular/router';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';

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
  constructor(
    private readonly router: Router,
    private readonly authService: AuthService
  ) {}
  @Input() isMenuOpened: boolean = false;
  @Output() toggleMenu = new EventEmitter<void>();

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  toggle() {
    this.isMenuOpened = !this.isMenuOpened;
    this.toggleMenu.emit();
  }
}
