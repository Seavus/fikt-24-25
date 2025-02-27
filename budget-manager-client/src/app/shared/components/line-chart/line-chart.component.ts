import { Component, Input } from '@angular/core';
import { Color, NgxChartsModule, ScaleType } from '@swimlane/ngx-charts';
import { ChartData } from './data-chart';

@Component({
  selector: 'app-line-chart',
  templateUrl: './line-chart.component.html',
  styleUrls: ['./line-chart.component.scss'],  
  standalone: true,
  imports: [NgxChartsModule]
})
export class LineChartComponent {
  @Input() data : ChartData[] = [];
  @Input() view: [number, number] = [700, 400]; 
  @Input() showXAxis = true;
  @Input() showYAxis = true;
  @Input() showLegend = false;
  @Input() showXAxisLabel = true;
  @Input() showYAxisLabel = true;
  @Input() xAxisLabel = 'Time';
  @Input() yAxisLabel = 'Value';
  @Input() autoScale = true;
  @Input() curve = 'linear';

  colorScheme: Color = {
    domain: ['#007bff', '#ff4081', '#4caf50', '#ff9800'],
    name: 'custom',
    selectable: true,
    group: ScaleType.Ordinal
  };
}
