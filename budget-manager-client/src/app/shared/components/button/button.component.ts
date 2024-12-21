import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-button',
  imports: [CommonModule],
  templateUrl: './button.component.html',
  styleUrl: './button.component.scss'
})
export class ButtonComponent {

@Input() text: string= '';
@Input() buttonClass: string = '';
@Output() onClick: EventEmitter<any> = new EventEmitter();

emitEvent() {
  this.onClick.emit();
}

}
