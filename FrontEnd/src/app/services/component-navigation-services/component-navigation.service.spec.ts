import { TestBed } from '@angular/core/testing';

import { ComponentNavigationService } from './component-navigation.service';

describe('NotificationsService', () => {
  let service: ComponentNavigationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ComponentNavigationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
