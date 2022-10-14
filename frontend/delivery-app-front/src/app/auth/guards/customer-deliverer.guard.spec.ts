import { TestBed } from '@angular/core/testing';

import { CustomerDelivererGuard } from './customer-deliverer.guard';

describe('CustomerDelivererGuard', () => {
  let guard: CustomerDelivererGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(CustomerDelivererGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
