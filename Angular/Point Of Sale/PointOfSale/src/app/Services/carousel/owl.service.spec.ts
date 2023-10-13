import { TestBed } from '@angular/core/testing';

import { OwlService } from './owl.service';

describe('OwlService', () => {
  let service: OwlService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OwlService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
