import { Component, Inject } from '@angular/core';
import {
  MAT_SNACK_BAR_DATA,
} from '@angular/material/snack-bar';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-snackbar',
  imports: [CommonModule],
  templateUrl: './snackbar.component.html',
  styleUrl: './snackbar.component.scss',
})
export class SnackbarComponent {
  constructor(@Inject(MAT_SNACK_BAR_DATA) public data: { message: string; type: string }) {}
}
