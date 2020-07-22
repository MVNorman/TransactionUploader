import { NgModule } from '@angular/core';

import { TransactionRoutingModule } from './transaction-routing.module';

import { TransactionValidatorService } from './service/transaction-validator.service';
import { TransactionService } from './service/transaction.service';

import { TransactionTableComponent } from './component/transaction-table/transaction-table.component';
import { TransactionTableFilterComponent } from './component/transaction-table-filter/transaction-table-filter.component';
import { TransactionUploadComponent } from './component/transaction-upload/transaction-upload.component';
import { TransactionRootComponent } from './component/transaction-root/transaction-root.component';
import { SharedModule } from '@shared/shared.module';


@NgModule({
  declarations: [
    TransactionRootComponent,
    TransactionTableComponent,
    TransactionTableFilterComponent,
    TransactionUploadComponent,
  ],
  imports: [
    SharedModule,
    TransactionRoutingModule
  ],
  providers: [
    TransactionValidatorService,
    TransactionService
  ]
})
export class TransactionModule { }
