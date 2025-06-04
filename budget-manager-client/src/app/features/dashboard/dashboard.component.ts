import { Component, OnInit } from '@angular/core';

import { BarChartComponent } from '../../shared/components/charts/bar-chart/bar-chart.component';
import { ChartData } from 'chart.js';
import { CommonModule } from '@angular/common';
import { DoughnutChartComponent } from '../../shared/components/charts/doughnut-chart/doughnut-chart.component';
import { LineChartComponent } from '../../shared/components/charts/line-chart/line-chart.component';
import { MatSelectModule } from '@angular/material/select';
import { StatisticsService } from '../../core/services/statistics.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    BarChartComponent,
    DoughnutChartComponent,
    LineChartComponent,
    MatSelectModule,
  ],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit {
  months = [
    { name: 'January', value: 1 },
    { name: 'February', value: 2 },
    { name: 'March', value: 3 },
    { name: 'April', value: 4 },
    { name: 'May', value: 5 },
    { name: 'June', value: 6 },
    { name: 'July', value: 7 },
    { name: 'August', value: 8 },
    { name: 'September', value: 9 },
    { name: 'October', value: 10 },
    { name: 'November', value: 11 },
    { name: 'December', value: 12 },
  ];
  years: number[] = [];

  selectedMonth = new Date().getMonth() + 1;
  selectedYear = new Date().getFullYear();

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
    this.generateYearOptions();
    this.fetchChartData();
  }

  generateYearOptions() {
    const currentYear = new Date().getFullYear();
    for (let i = currentYear - 5; i <= currentYear + 5; i++) {
      this.years.push(i);
    }
  }

  onDateChange() {
    this.fetchChartData();
  }

  fetchChartData() {
    this.doughnutChartData = { labels: [], datasets: [] };
    this.lineChartData = { labels: [], datasets: [] };
    this.barChartData = { labels: [], datasets: [] };

    this.statsService
      .getStatistics(this.selectedMonth, this.selectedYear)
      .subscribe((data) => {
        if (data?.transactionsByCategory?.length) {
          this.prepareDoughnutData(data.transactionsByCategory);
        }
        if (data?.transactionsByDay?.length) {
          this.prepareLineChartData(data.transactionsByDay);
          this.prepareBarChartData(data.transactionsByDay);
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

  prepareBarChartData(dayArray: { date: string; amount: number }[]) {
    this.barChartData = {
      labels: dayArray.map((d) => d.date),
      datasets: [
        {
          label: 'Amount',
          data: dayArray.map((d) => d.amount),
        },
      ],
    };
  }
}
