import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'home',
    loadComponent: () =>
      import('./features/home/home.component').then((c) => c.HomeComponent),
  },
  {
    path: 'transactions',
    loadComponent: () =>
      import('./features/transactions/transactions.component').then(
        (c) => c.TransactionsComponent
      ),
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
    data: { showSidenav: true },
  },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
];
