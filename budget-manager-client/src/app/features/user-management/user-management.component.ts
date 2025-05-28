import { Component, DestroyRef, OnInit, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';

import { ButtonComponent } from '../../shared/components/button/button.component';
import { CommonModule } from '@angular/common';
import { InputComponent } from '../../shared/components/input/input.component';
import { SnackbarService } from '../../core/services/snackbar.service';
import { UserService } from '../../core/services/user.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-user-management',
  imports: [CommonModule, ReactiveFormsModule, ButtonComponent, InputComponent],
  templateUrl: './user-management.component.html',
  styleUrl: './user-management.component.scss',
})
export class UserManagementComponent implements OnInit {
  form: FormGroup;
  private readonly fb = inject(FormBuilder);
  private readonly userService = inject(UserService);
  private readonly snackbar = inject(SnackbarService);
  private readonly destroyRef = inject(DestroyRef);

  constructor() {
    this.form = this.fb.group({
      firstName: [''],
      lastName: [''],
    });
  }
  ngOnInit(): void {
    const userId = localStorage.getItem('userId');

    if (!userId) return;

    this.userService
      .getUserById(userId)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((data) => {
        this.form.patchValue({
          firstName: data.firstName,
          lastName: data.lastName,
        });
      });
  }

  onUpdate(): void {
    if (this.form.invalid) return;

    const userId = localStorage.getItem('userId');
    if (!userId) return;

    const { firstName, lastName } = this.form.value;
    this.userService
      .updateUser({ userId, firstName, lastName })
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: () => {
          this.snackbar.showSnackbar('User updated successfully!', 'success');
        },
        error: () => this.snackbar.showSnackbar('Update failed.', 'error'),
      });
  }
}
