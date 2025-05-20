import { Routes } from '@angular/router';
import { authGuard } from './auth.guard';

export const routes: Routes = [
  {
    path: 'dashboard',
    loadComponent: () =>
      import('./features/dashboard/dashboard.component').then(
        (c) => c.DashboardComponent
      ),
    canActivate: [authGuard],
  },
  {
    path: 'transactions',
    loadComponent: () =>
      import('./features/transactions/transactions.component').then(
        (c) => c.TransactionsComponent
      ),
    canActivate: [authGuard],
  },
  {
    path: 'login',
    loadComponent: () =>
      import('./features/login/login.component').then((c) => c.LoginComponent),
  },
  {
    path: 'register',
    loadComponent: () =>
      import('./features/registration/registration.component').then(
        (c) => c.RegistrationComponent
      ),
  },
  {
    path: 'user-management',
    loadComponent: () =>
      import('./features/user-management/user-management.component').then(
        (c) => c.UserManagementComponent
      ),
  },
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
];
