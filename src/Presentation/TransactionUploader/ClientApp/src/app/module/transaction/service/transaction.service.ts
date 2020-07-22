import { Injectable } from '@angular/core';
import { TransactionHttpService } from '@data/http/transaction/transaction-http.service';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Injectable()
export class TransactionService {

  private uploadErrorSubject$ = new Subject<string>();
  private uploadSuccessSubject$ = new Subject<string>();

  uploadError$ = this.uploadErrorSubject$.asObservable();
  uploadSuccess$ = this.uploadSuccessSubject$.asObservable();

  unsubscibe$ = new Subject<void>();

  constructor(private transactionHttpService: TransactionHttpService) {
  }

  uploadFile(formData: FormData): void {
    this.transactionHttpService.uploadFile(formData)
    .pipe(takeUntil(this.unsubscibe$))
    .subscribe(() => {
      this.uploadSuccessSubject$.next('Successfully uploaded');
    }, (errorHttpResponse: any) => {
      this.uploadErrorSubject$.next(errorHttpResponse.error);
    })
  }
}
