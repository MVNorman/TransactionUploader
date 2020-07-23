import { TestBed } from '@angular/core/testing';

import { TransactionValidatorService } from './transaction-validator.service';

describe('TransactionValidatorService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TransactionValidatorService = TestBed.get(TransactionValidatorService);
    expect(service).toBeTruthy();
  });
});
