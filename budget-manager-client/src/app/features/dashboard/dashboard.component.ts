import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BarChartComponent } from '../../shared/components/charts/bar-chart/bar-chart.component';
import { DoughnutChartComponent } from '../../shared/components/charts/doughnut-chart/doughnut-chart.component';
import { LineChartComponent } from '../../shared/components/charts/line-chart/line-chart.component';
import { StatisticsService } from '../../core/services/statistics.service';
import { ChartData } from 'chart.js';

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
export class DashboardComponent implements OnInit {
  doughnutChartData: ChartData<'doughnut', number[], string> = {
    labels: [],
    datasets: [],
  };
  barChartData: ChartData<'bar', number[], string> = {
    labels: [],
    datasets: [],
  };

  lineChartData: ChartData<'line', number[], string> = {
    labels: [],
    datasets: [],
  };

  constructor(private readonly statsService: StatisticsService) {}

  ngOnInit() {
    this.statsService.getStatistics(5, 2025).subscribe((data) => {
      console.log('API response:', data);

      if (data?.transactionsByCategory?.length) {
        this.prepareDoughnutData(data.transactionsByCategory);
      }

      if (data?.transactionsByDay?.length) {
        this.prepareLineChartData(data.transactionsByDay);
      }
    });
  }

  prepareDoughnutData(
    categoryArray: { categoryName: string; count: number }[]
  ) {
    this.doughnutChartData = {
      labels: categoryArray.map((c) => c.categoryName),
      datasets: [
        {
          label: 'Categories',
          data: categoryArray.map((c) => c.count),
          backgroundColor: [
            'rgb(122, 186, 120)',
            'rgb(246, 233, 178)',
            'rgb(65, 122, 209)',
            'rgb(10, 104, 71)',
            'rgb(243, 202, 82)',
          ],
        },
      ],
    };
  }

  prepareLineChartData(dayArray: { date: string; amount: number }[]) {
    const sorted = [...dayArray].sort(
      (a, b) => new Date(a.date).getTime() - new Date(b.date).getTime()
    );

    this.lineChartData = {
      labels: sorted.map((d) => d.date) || [],
      datasets: [
        {
          label: 'Daily Amount',
          data: sorted.map((d) => d.amount),
          borderColor: 'rgb(10, 104, 71)',
          fill: false,
          tension: 0.1,
        },
      ],
    };
  }
}
