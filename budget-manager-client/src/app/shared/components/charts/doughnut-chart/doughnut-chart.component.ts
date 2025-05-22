import { Component, Input, AfterViewInit } from '@angular/core';
import { Chart, ChartConfiguration, ChartData, registerables } from 'chart.js';

Chart.register(...registerables);

@Component({
  selector: 'app-doughnut-chart',
  imports: [],
  templateUrl: './doughnut-chart.component.html',
  styleUrl: './doughnut-chart.component.scss',
})
export class DoughnutChartComponent implements AfterViewInit {
  @Input() chartId: string = 'doughnutChart';
  @Input() data!: ChartData<'doughnut', number[], string>;

  private chart!: Chart<'doughnut'>;

  ngAfterViewInit(): void {
    this.createChart();
  }

  createChart() {
    if (!this.data?.labels?.length || !this.data?.datasets?.length) {
      console.error(
        'DoughnutChartComponent: labels and datasets are required!'
      );
      return;
    }

    const config: ChartConfiguration<'doughnut'> = {
      type: 'doughnut',
      data: this.data,
      options: {
        responsive: true,
      },
    };

    this.chart = new Chart(this.chartId, config);
  }
}
