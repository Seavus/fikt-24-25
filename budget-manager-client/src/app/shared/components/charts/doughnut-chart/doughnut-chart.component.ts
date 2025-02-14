import { Component, Input, AfterViewInit } from '@angular/core';
import { Chart, ChartConfiguration, registerables } from 'chart.js';

Chart.register(...registerables);

@Component({
  selector: 'app-doughnut-chart',
  imports: [],
  templateUrl: './doughnut-chart.component.html',
  styleUrl: './doughnut-chart.component.scss'
})
export class DoughnutChartComponent implements AfterViewInit{

  @Input() chartId: string = 'doughnutChart';
  @Input() labels: string[] = [];
  @Input() datasets: any[] = [];

  chart: any;

  ngAfterViewInit(): void {
    this.createChart();
  }

  createChart() {
    if (!this.labels.length || !this.datasets.length) {
      console.error('DoughnutChartComponent: labels and datasets are required!');
      return;
    }

    const config: ChartConfiguration<'doughnut'> = {
      type: 'doughnut',
      data: {
        labels: this.labels,
        datasets: this.datasets,
      },
      options: {
        responsive: true,
      },
    };

    this.chart = new Chart(this.chartId, config);
  }

}
