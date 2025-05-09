import { AfterViewInit, Component, Input } from '@angular/core';
import { Chart, ChartConfiguration } from 'chart.js';

@Component({
  selector: 'app-line-chart',
  standalone: true,
  templateUrl: './line-chart.component.html',
  styleUrls: ['./line-chart.component.scss'],
})
export class LineChartComponent implements AfterViewInit {
  @Input() chartId: string = 'lineChart';
  @Input() data!: {
    labels: string[];
    datasets: any[];
  };

  private chart!: Chart;

  ngAfterViewInit(): void {
    this.createChart();
  }

  createChart(): void {
    if (!this.data?.labels?.length || !this.data?.datasets?.length) {
      console.error('LineChartComponent: labels and datasets are required!');
      return;
    }

    const config: ChartConfiguration<'line'> = {
      type: 'line',
      data: this.data,
      options: {
        responsive: true,
        scales: {
          x: { type: 'category' },
          y: { beginAtZero: true },
        },
      },
    };

    this.chart = new Chart(this.chartId, config);
  }
}
