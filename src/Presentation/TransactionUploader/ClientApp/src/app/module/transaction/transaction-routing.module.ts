import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { TransactionRootComponent } from './transaction-root/transaction-root.component';

const routes: Routes = [
  {
    path: '',
    component: TransactionRootComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TransactionRoutingModule { }