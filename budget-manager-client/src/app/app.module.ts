import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatTableModule } from '@angular/material/table'; 
import { MatButtonModule } from '@angular/material/button';  // Correct import

import { AppComponent } from './app.component';
import { TransactionComponent } from './shared/components/transaction-component/transaction-component';

@NgModule({
  declarations: [
    MatButtonModule,  // For buttons
    MatTableModule,
    AppComponent,
   TransactionComponent
  ],
  imports: [
    BrowserModule,
    MatTableModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
