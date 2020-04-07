import { TestBed } from '@angular/core/testing';

import { CategoryEditGuard } from './category-edit.guard';

describe('CategoryEditGuard', () => {
  let guard: CategoryEditGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(CategoryEditGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
