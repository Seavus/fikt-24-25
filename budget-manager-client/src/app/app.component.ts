import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from "./shared/components/button/button.component";

@Component({
  selector: 'app-root',
  imports: [ CommonModule, ButtonComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'budget-manager-client';
}
