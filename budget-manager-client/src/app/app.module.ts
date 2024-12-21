import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatTableModule } from '@angular/material/table'; 
import { MatButtonModule } from '@angular/material/button';

import { AppComponent } from './app.component';
import { TransactionComponent } from './shared/components/transaction-component/transaction-component';

@NgModule({
  imports: [
    AppComponent,
    TransactionComponent,
    BrowserModule,       
    MatTableModule,      
    MatButtonModule, 
  ],
  providers: [] 
})
export class AppModule { }
