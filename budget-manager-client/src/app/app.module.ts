import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatTableModule } from '@angular/material/table'; 
import { MatButtonModule } from '@angular/material/button';

import { AppComponent } from './app.component';
import { TransactionsComponent } from './shared/components/transactions/transactions.component';

@NgModule({
  imports: [
    AppComponent,
    TransactionsComponent,
    BrowserModule,       
    MatTableModule,      
    MatButtonModule, 
  ],
  providers: [] 
})
export class AppModule { }