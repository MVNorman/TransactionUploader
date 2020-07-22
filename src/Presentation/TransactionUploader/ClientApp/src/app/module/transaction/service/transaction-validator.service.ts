import { Injectable } from '@angular/core';

@Injectable()
export class TransactionValidatorService {

  private supportedMimeTypes: string[]


  constructor() {
    this.initMimeTypes();
  }

  isMimeTypeSupported(mimeType: string): boolean {
    return this.supportedMimeTypes.some(type => type === mimeType);
  }

  private initMimeTypes(): void {
    this.supportedMimeTypes = [
      'application/vnd.ms-excel',
      'text/csv',
      'text/xml',
      'application/xml'
    ]
  }
}
