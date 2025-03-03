import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SnackbarComponent } from '../../shared/components/snackbar/snackbar.component';

@Injectable({
  providedIn: 'root'
})
export class SnackbarService {

  constructor(private readonly snackBar: MatSnackBar) { }

  showSnackbar(message: string, type: 'success' | 'error' | 'info', duration: number = 3000) {
    this.snackBar.openFromComponent(SnackbarComponent, {
      data: { message, type },
      duration,
      horizontalPosition: 'end', 
      verticalPosition: 'top',
      panelClass: [type], 
    });
  }
}

