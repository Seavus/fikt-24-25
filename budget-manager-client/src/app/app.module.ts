import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatTableModule } from '@angular/material/table'; 


import { AppComponent } from './app.component';
import { TransactionComponent } from './transaction-component/transaction-component';
import { MatButtonModule } from '@angular/material/button';


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
