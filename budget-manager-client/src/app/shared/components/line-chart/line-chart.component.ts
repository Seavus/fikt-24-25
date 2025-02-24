import { Component, OnInit } from '@angular/core';
import { Chart, ChartData, ChartOptions, ChartConfiguration } from 'chart.js';

@Component({
  selector: 'app-line-chart',
  standalone: true,
  templateUrl: './line-chart.component.html',
  styleUrls: ['./line-chart.component.scss']
})
export class LineChartComponent implements OnInit {
  chart!: Chart<'line', (number | null)[], string>;

  constructor() {}

  ngOnInit(): void {
    this.createChart();
  }

  createChart(): void {
    // Define chart data
    const chartData: ChartData<'line'> = {
      labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
      datasets: [{
        label: 'My First dataset',
        data: [65, 59, 80, 81, 56, 55, 40], 
        fill: false,
        borderColor: 'rgb(75, 192, 192)',
        tension: 0.1
      }]
    };

    // Define chart options
    const chartOptions: ChartOptions<'line'> = {
      responsive: true,
      scales: {
        x: {
          type: 'category', 
        },
        y: {
          type: 'linear', 
          beginAtZero: true 
        }
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
