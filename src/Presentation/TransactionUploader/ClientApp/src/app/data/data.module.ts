import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TransactionHttpService } from './http/transaction/transaction-http.service';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  providers:[
    TransactionHttpService
  ]
})
export class DataModule { }