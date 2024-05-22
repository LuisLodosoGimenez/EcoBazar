import { TestBed } from '@angular/core/testing';

import { CreateOrderApiService } from './create-order-api.service';

describe('CreateOrderApiService', () => {
  let service: CreateOrderApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CreateOrderApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
