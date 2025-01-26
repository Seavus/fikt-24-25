import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-dynamic-popup',
  imports: [CommonModule],
  standalone: true,
  templateUrl: './dynamic-popup.component.html',
  styleUrl: './dynamic-popup.component.scss'
})
export class DynamicPopupComponent {
  @Input() isOpen: boolean = false;
  @Input() popupTitle: string = '';
  @Input() popupData: any = null; 
  @Output() closePopup: EventEmitter<void> = new EventEmitter<void>();

  close() {
    this.closePopup.emit();

}
}
