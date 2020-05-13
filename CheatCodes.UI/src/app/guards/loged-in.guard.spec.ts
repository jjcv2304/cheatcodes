import { TestBed } from '@angular/core/testing';

import { LoggedInGuard } from './logged-in.guard';
import {AuthService} from '../security/auth-service.component';
import {HttpClientTestingModule} from '@angular/common/http/testing';

describe('LogedInGuard', () => {
  let guard: LoggedInGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        {provide: AuthService},

        ]
    }).compileComponents();
    guard = TestBed.inject(LoggedInGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
