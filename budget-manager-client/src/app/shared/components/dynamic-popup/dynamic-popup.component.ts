import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import {
  MatDialogActions,
  MatDialogClose,
  MatDialogRef,
  MatDialog,
  MatDialogContent

} from '@angular/material/dialog';

@Component({
  selector: 'app-dynamic-popup',
  styleUrl: 'dynamic-popup.component.scss',
  templateUrl: 'dynamic-popup.component.html',
  imports: [MatButtonModule],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DynamicPopupComponent {
  readonly dialog = inject(MatDialog);

  openDialog(enterAnimationDuration: string, exitAnimationDuration: string): void {
    this.dialog.open(DynamicPopupWrapper, {
      width: '250px',
      enterAnimationDuration,
      exitAnimationDuration,
    });
  }
}

@Component({
  selector: 'dynamic-popup-wrapper',
  templateUrl: 'dynamic-popup-wrapper.html',
  imports: [MatButtonModule, MatDialogActions, MatDialogClose, MatDialogContent],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DynamicPopupWrapper {
  readonly dialogRef = inject(MatDialogRef<DynamicPopupWrapper>);
}


