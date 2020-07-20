import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TransactionRoutingModule } from './transaction-routing.module';

import { TransactionTableComponent } from './transaction-table/transaction-table.component';
import { TransactionTableFilterComponent } from './transaction-table-filter/transaction-table-filter.component';
import { TransactionUploadComponent } from './transaction-upload/transaction-upload.component';
import { TransactionRootComponent } from './transaction-root/transaction-root.component';

@NgModule({
  declarations: [
    TransactionRootComponent,
    TransactionTableComponent,
    TransactionTableFilterComponent,
    TransactionUploadComponent,
  ],
  imports: [
    CommonModule,
    TransactionRoutingModule
  ]
})
export class TransactionModule { }
