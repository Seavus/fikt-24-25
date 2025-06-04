import {
  Component,
  DestroyRef,
  EventEmitter,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { Router, RouterModule } from '@angular/router';

import { AuthService } from '../../services/auth.service';
import { ButtonComponent } from '../../shared/components/button/button.component';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { Observable } from 'rxjs';
import { User } from '../interfaces/user.interface';
import { UserService } from '../services/user.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

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
  user!: User;
  constructor(
    private readonly router: Router,
    private readonly authService: AuthService,
    private readonly userService: UserService,
    private readonly destroyRef: DestroyRef
  ) {}
  @Input() isMenuOpened: boolean = false;
  @Output() toggleMenu = new EventEmitter<void>();

  ngOnInit(): void {
    this.balance$ = this.userService.balance$;
    const userId = localStorage.getItem('userId');

    if (!userId) return;

    this.userService
      .getUserById(userId)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((data) => {
        this.user = data;
        const balance = data.balance ?? 0;
        this.userService.setBalance(balance);
      });
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
