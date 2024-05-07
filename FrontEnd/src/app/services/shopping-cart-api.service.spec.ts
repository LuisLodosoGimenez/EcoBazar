import { TestBed } from '@angular/core/testing';

import { ShoppingCartApiService } from './shopping-cart-api.service';

describe('ShoppingCartApiService', () => {
  let service: ShoppingCartApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ShoppingCartApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
