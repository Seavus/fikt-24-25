import { AfterViewInit, Component, Input, OnDestroy } from '@angular/core';
import { Chart, ChartConfiguration, ChartData } from 'chart.js';

@Component({
  selector: 'app-line-chart',
  standalone: true,
  templateUrl: './line-chart.component.html',
  styleUrls: ['./line-chart.component.scss'],
})
export class LineChartComponent implements AfterViewInit, OnDestroy {
  @Input() chartId: string = 'lineChart';
  @Input() data!: ChartData<'line', number[], string>;

  private chart!: Chart<'line'> | undefined;

  ngAfterViewInit(): void {
    this.createChart();
  }

  ngOnDestroy(): void {
    this.destroyChart();
  }

  private destroyChart(): void {
    if (this.chart) {
      this.chart.destroy();
      this.chart = undefined;
    }
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
