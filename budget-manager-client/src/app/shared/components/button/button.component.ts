import { Component, Input, Output, EventEmitter } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-button',
  imports: [MatButtonModule, CommonModule],
  templateUrl: './button.component.html',
  styleUrl: './button.component.scss'
})
export class ButtonComponent {

@Input() text: string= '';
@Input() customButton: string = '';
@Output() onClick: EventEmitter<void> = new EventEmitter();

emitEvent() {
  this.onClick.emit();
}

}