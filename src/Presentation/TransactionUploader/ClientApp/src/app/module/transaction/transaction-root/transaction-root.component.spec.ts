import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TransactionRootComponent } from './transaction-root.component';

describe('TransactionRootComponent', () => {
  let component: TransactionRootComponent;
  let fixture: ComponentFixture<TransactionRootComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TransactionRootComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TransactionRootComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
