import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuard } from '@core/guard/auth.guard';

import { ContentLayoutComponent } from '@layout/content-layout/content-layout.component';
import { PageNotFoundComponent } from '@layout/page-not-found/page-not-found.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: '/transaction',
    pathMatch: 'full'
  },
  {
    path: '',
    canActivate: [AuthGuard],
    component: ContentLayoutComponent,
    children: [
      {
        path: 'transaction',
        loadChildren: () =>
          import('@module/transaction/transaction.module').then(module => module.TransactionModule)
      }
    ]
  },
  { 
    path: '**',
    component: PageNotFoundComponent,
    pathMatch: 'full' 
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
