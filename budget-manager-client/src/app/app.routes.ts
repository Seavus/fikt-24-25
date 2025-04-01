import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'home',
    loadComponent: () =>
      import('./features/home/home.component').then((c) => c.HomeComponent),
    data: { showSidenav: true },
  },
  {
    path: 'transactions',
    loadComponent: () =>
      import('./features/transactions/transactions.component').then(
        (c) => c.TransactionsComponent
      ),
    data: { showSidenav: true },
  },
  {
    path: 'login',
    loadComponent: () =>
      import('./features/login/login.component').then((c) => c.LoginComponent),
    data: { showSidenav: false },
  },
  {
    path: 'register',
    loadComponent: () =>
      import('./features/registration/registration.component').then(
        (c) => c.RegistrationComponent
      ),
    data: { showSidenav: false },
  },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
];
