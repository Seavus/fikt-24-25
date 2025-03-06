import { Component, input } from '@angular/core';
import { dashboardModel } from '../dashboard-wrapper/dashboard-wrapper.component';
import { MatOptionModule } from '@angular/material/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';  


@Component({
  selector: 'app-widget',
  imports: [MatOptionModule, CommonModule, MatCardModule],
  templateUrl: './widget.component.html',
  styleUrl: './widget.component.scss'
})
export class WidgetComponent {
  data = input.required<dashboardModel>();

}
