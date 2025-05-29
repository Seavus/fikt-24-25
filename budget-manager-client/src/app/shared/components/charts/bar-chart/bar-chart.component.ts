import { Component, Input, AfterViewInit, OnDestroy } from '@angular/core';
import { Chart, ChartConfiguration, ChartData, registerables } from 'chart.js';

Chart.register(...registerables);

@Component({
  selector: 'app-bar-chart',
  templateUrl: './bar-chart.component.html',
  styleUrls: ['./bar-chart.component.scss'],
})
export class BarChartComponent implements AfterViewInit, OnDestroy {
  @Input() chartId: string = 'barChart';

  @Input() data!: ChartData<'bar', number[], string>;

  private chart!: Chart<'bar'> | undefined;

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
  createChart() {
    if (!this.data?.labels?.length || !this.data?.datasets?.length) {
      console.error('BarChartComponent: labels and datasets are required!');
      return;
    }

    const config: ChartConfiguration<'bar'> = {
      type: 'bar',
      data: this.data,
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
