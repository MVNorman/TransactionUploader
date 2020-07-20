import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthGuard } from './guard/auth.guard';

import { throwIfAlreadyLoaded } from './guard/module-import.guard';

@NgModule({
    declarations: [],
    imports: [
      CommonModule
    ],
    providers: [      
      AuthGuard
    ]
  })
  export class CoreModule {
    constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
      throwIfAlreadyLoaded(parentModule, 'CoreModule');
    }
  }