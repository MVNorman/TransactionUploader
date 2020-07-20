import { TestBed } from '@angular/core/testing';

import { TransactionHttpService } from './transaction-http.service';

describe('TransactionHttpService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TransactionHttpService = TestBed.get(TransactionHttpService);
    expect(service).toBeTruthy();
  });
});
