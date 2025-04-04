import { Component, Input, OnInit } from '@angular/core';
import { Chart, ChartData, ChartOptions, ChartConfiguration } from 'chart.js';

@Component({
  selector: 'app-line-chart',
  standalone: true,
  templateUrl: './line-chart.component.html',
  styleUrls: ['./line-chart.component.scss']
})
export class LineChartComponent implements OnInit {
  @Input() labels: string[] = [];
  @Input() data: (number | null)[] = [];
  @Input() label: string = 'Dataset';
  @Input() borderColor: string = 'rgb(75, 192, 192)';

  chart!: Chart<'line', (number | null)[], string>;

  constructor() {}

  ngOnInit(): void {
    this.createChart();
  }

  createChart(): void {
    const chartData: ChartData<'line'> = {
      labels: this.labels,
      datasets: [{
        label: this.label,
        data: this.data,
        fill: false,
        borderColor: this.borderColor,
        tension: 0.1
      }]
    };

    const chartOptions: ChartOptions<'line'> = {
      responsive: true,
      scales: {
        x: { type: 'category' },
        y: { type: 'linear', beginAtZero: true }
      }
    };

    const chartConfig: ChartConfiguration<'line'> = {
      type: 'line',
      data: chartData,
      options: chartOptions
    };

    new Chart('canvas', chartConfig);
  }
}