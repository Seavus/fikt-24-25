import { Component } from '@angular/core';
import { ButtonComponent } from '../../shared/components/button/button.component';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  imports: [ButtonComponent, RouterModule],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.scss'
})
export class SidebarComponent {

}
