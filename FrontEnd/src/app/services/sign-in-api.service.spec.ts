import { TestBed } from '@angular/core/testing';

import { SignInApiService } from './sign-in-api.service';

describe('SignInApiService', () => {
  let service: SignInApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SignInApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
