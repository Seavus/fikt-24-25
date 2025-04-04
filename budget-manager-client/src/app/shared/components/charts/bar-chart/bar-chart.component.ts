import { Component, Input, AfterViewInit } from '@angular/core';
import { Chart, ChartConfiguration, registerables, ChartDataset } from 'chart.js';

Chart.register(...registerables);

@Component({
  selector: 'app-bar-chart',
  templateUrl: './bar-chart.component.html',
  styleUrls: ['./bar-chart.component.scss'],
})
export class BarChartComponent implements AfterViewInit {
  @Input() chartId: string = 'barChart';
  @Input() labels: string[] = [];
  @Input() datasets: ChartDataset<'bar'>[] = [];

  private chart!: Chart; 

  ngAfterViewInit(): void {
    this.createChart();
  }
  
  createChart() {
    if (!this.labels.length || !this.datasets.length) {
      console.error('BarChartComponent: labels and datasets are required!');
      return;
    }

    const config: ChartConfiguration<'bar'> = {
      type: 'bar',
      data: {
        labels: this.labels,
        datasets: this.datasets,
      },
      options: {
        responsive: true,
        scales: {
          y: {
            beginAtZero: true,
          },
        },
      },
    };

    this.chart = new Chart(this.chartId, config);
  }
}
