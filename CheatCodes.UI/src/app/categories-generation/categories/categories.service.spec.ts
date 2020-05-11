import {TestBed, inject} from '@angular/core/testing';

import {CategoriesService} from './categories.service';
import {HttpClientTestingModule} from '@angular/common/http/testing';

describe('CategoriesService',
  () => {
    beforeEach(() => {
      TestBed.configureTestingModule({
        imports: [HttpClientTestingModule],
        providers: [CategoriesService]
      });
    });

    it('should be created',
      inject([CategoriesService],
        (service: CategoriesService) => {
          expect(service).toBeTruthy();
        }));
  });
