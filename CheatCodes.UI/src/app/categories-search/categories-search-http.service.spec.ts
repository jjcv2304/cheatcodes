import { TestBed } from '@angular/core/testing';

import { CategoriesSearchHttpService } from './categories-search-http.service';

describe('CategoriesSearchHttpService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CategoriesSearchHttpService = TestBed.get(CategoriesSearchHttpService);
    expect(service).toBeTruthy();
  });
});
