import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { AuthService, currentUserSignal } from '../../services/auth.service';
import { SnackbarService } from '../../core/services/snackbar.service';
import { UserService } from '../../core/services/user.service';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from '../../shared/components/button/button.component';
import { InputComponent } from '../../shared/components/input/input.component';

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
  private readonly authService = inject(AuthService);
  private readonly snackbar = inject(SnackbarService);
  private readonly destroyRef = inject(DestroyRef);

  constructor() {
    this.form = this.fb.group({
      firstName: [''],
      lastName: [''],
    });
  }
  ngOnInit(): void {
    const userId = currentUserSignal();
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

    const userId = currentUserSignal();
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
