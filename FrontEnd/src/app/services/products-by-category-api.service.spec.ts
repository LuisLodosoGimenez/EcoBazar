import { TestBed } from '@angular/core/testing';

import { ProductsByCategoryApiService } from './products-by-category-api.service';

describe('ProductsByCategoryApiService', () => {
  let service: ProductsByCategoryApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProductsByCategoryApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
