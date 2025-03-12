import { Component, inject, Type } from '@angular/core';
import { WidgetComponent } from '../widget/widget.component';
import { ButtonComponent } from '../button/button.component';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';  
import { BoxWrapperComponent } from '../box-wrapper/box-wrapper.component';

export interface dashboardModel {
  id: number;
  label: string;
  content: Type<unknown>;
}

@Component({
  selector: 'app-dashboard-wrapper',
  imports: [WidgetComponent, CommonModule, MatCardModule],
  templateUrl: './dashboard-wrapper.component.html',
  styleUrl: './dashboard-wrapper.component.scss'
})
export class DashboardWrapperComponent {
  store = {
    models: [
      { id: 1, label: 'Dashboard 1', content: undefined },
    ]
  };

  trackById(index: number, item: dashboardModel): number {
    return item.id;
  }
}