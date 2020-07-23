import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators} from '@angular/forms';
import { takeUntil } from 'rxjs/operators';

import { TransactionValidatorService } from '@module/transaction/service/transaction-validator.service';
import { TransactionService } from '@module/transaction/service/transaction.service';

@Component({
  selector: 'app-transaction-upload',
  templateUrl: './transaction-upload.component.html',
  styleUrls: ['./transaction-upload.component.css']
})
export class TransactionUploadComponent implements OnDestroy, OnInit {

  uploadForm: FormGroup = new FormGroup({
    file: new FormControl('', [Validators.required]),
    source: new FormControl('', [Validators.required])
  });
    
  constructor(
    private transactionValidator: TransactionValidatorService,
    private transactionService: TransactionService) { }

  ngOnInit(): void {
    this.onErrorUploadSub();
    this.onSuccessUploadSub();
  }

  ngOnDestroy(): void {
    this.transactionService.unsubscibe$.next();
    this.transactionService.unsubscibe$.complete();
  }

  onFileChange(event): void {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      var isMimeTypeSupported = this.transactionValidator.isMimeTypeSupported(file.type);
      if(isMimeTypeSupported){
        this.uploadForm.patchValue({
          source: file
        });
      }
      else {
        this.uploadForm.patchValue({
          file: ''
        });
      }
    }
  }
     
  submit(): void {
    const formData = new FormData();
    formData.append('formFile', this.uploadForm.get('source').value);
    this.transactionService.uploadFile(formData);
  }

  onErrorUploadSub(): void {
    this.transactionService.uploadError$
    .pipe(takeUntil(this.transactionService.unsubscibe$))
    .subscribe((error: string)=> {
      alert(error);
    })
  }

  onSuccessUploadSub(): void {
    this.transactionService.uploadSuccess$
    .pipe(takeUntil(this.transactionService.unsubscibe$))
    .subscribe((message: string)=> {
      alert(message);
    })
  }

}
