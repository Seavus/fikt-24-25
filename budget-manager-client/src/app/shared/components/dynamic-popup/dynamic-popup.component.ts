import { ChangeDetectionStrategy, Component, Inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import {
  MatDialogActions,
  MatDialogRef,
  MatDialog,
  MatDialogContent,
  MAT_DIALOG_DATA,
} from '@angular/material/dialog';
import { PopupData } from './popup-data.model';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-dynamic-popup',
  styleUrl: 'dynamic-popup.component.scss',
  templateUrl: 'dynamic-popup.component.html',
  imports: [MatButtonModule],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DynamicPopupComponent {
  constructor(private readonly dialog: MatDialog) {}

  openDialog(
    enterAnimationDuration: string,
    exitAnimationDuration: string
  ): void {
    const data = new PopupData({
      title: 'Enter Your Info',
      inputLabel: 'Type here',
      showInput: true,
    });

    this.dialog.open(DynamicPopupWrapper, {
      width: '300px',
      enterAnimationDuration,
      exitAnimationDuration,
      data,
    });
  }
}

@Component({
  selector: 'dynamic-popup-wrapper',
  templateUrl: 'dynamic-popup-wrapper.html',
  imports: [
    MatButtonModule,
    MatDialogActions,
    MatDialogContent,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    CommonModule,
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DynamicPopupWrapper {
  inputValue: string = '';

  constructor(
    public dialogRef: MatDialogRef<DynamicPopupWrapper>,
    @Inject(MAT_DIALOG_DATA) public data: PopupData
  ) {}

  onConfirm(): void {
    if (this.data.showInput) {
      this.dialogRef.close(this.inputValue);
    } else {
      this.dialogRef.close(true);
    }
  }

  onCancel(): void {
    this.dialogRef.close(false);
  }
}
