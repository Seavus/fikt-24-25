import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ButtonComponent } from '../../shared/components/button/button.component';
import { Router, RouterModule } from '@angular/router';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { UserStateService } from '../services/user-state.service';
import { Observable } from 'rxjs';

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
export class SidebarComponent implements OnInit {
  balance$!: Observable<number>;
  constructor(
    private readonly router: Router,
    private readonly authService: AuthService,
    private readonly userStateService: UserStateService
  ) {}
  @Input() isMenuOpened: boolean = false;
  @Output() toggleMenu = new EventEmitter<void>();

  ngOnInit() {
    this.balance$ = this.userStateService.balance$;
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  toggle() {
    this.isMenuOpened = !this.isMenuOpened;
    this.toggleMenu.emit();
  }
}
