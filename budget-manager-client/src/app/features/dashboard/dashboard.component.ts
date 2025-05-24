import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { barChartData, lineChartData, doughnutChartData } from './chart-data';
import { BarChartComponent } from '../../shared/components/charts/bar-chart/bar-chart.component';
import { DoughnutChartComponent } from '../../shared/components/charts/doughnut-chart/doughnut-chart.component';
import { LineChartComponent } from '../../shared/components/line-chart/line-chart.component';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    BarChartComponent,
    DoughnutChartComponent,
    LineChartComponent,
  ],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent {
  barChartData = barChartData;
  lineChartData = lineChartData;
  doughnutChartData = doughnutChartData;
}
