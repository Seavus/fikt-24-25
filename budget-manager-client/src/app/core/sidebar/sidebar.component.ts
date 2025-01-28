import { Component } from '@angular/core';
import { ButtonComponent } from '../../shared/components/button/button.component';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  imports: [ButtonComponent, RouterModule],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.scss'
})
export class SidebarComponent {
  constructor(private readonly router: Router) {}

  logout() {
    this.router.navigate(['/login']);
  }
}
