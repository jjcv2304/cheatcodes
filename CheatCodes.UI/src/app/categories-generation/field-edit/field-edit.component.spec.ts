import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {FieldEditComponent} from './field-edit.component';
import {HttpClientTestingModule} from '@angular/common/http/testing';
import {RouterTestingModule} from '@angular/router/testing';
import {CategoriesService} from '../categories/categories.service';
import {ActivatedRoute} from '@angular/router';
import {FormsModule} from '@angular/forms';
import {createSpyObj} from '../../test-utils/testUtils';

describe('FieldEditComponent',
  () => {
    let fixture: ComponentFixture<FieldEditComponent>;
    let mockActivatedRoute;
    let mockCategoryService;

    beforeEach(async(() => {
      mockActivatedRoute = {snapshot: {paramMap: {get: () => false}}};
      mockCategoryService = createSpyObj('CategoriesService', ['SetCategoryFilter', 'RefreshCategoryLastFilter', 'GetCurrentCategories']);

      TestBed.configureTestingModule({
        imports: [HttpClientTestingModule, RouterTestingModule.withRoutes([]), FormsModule],
        declarations: [FieldEditComponent],
        providers: [
          {provide: CategoriesService, useValue: mockCategoryService},
          {provide: ActivatedRoute, useValue: mockActivatedRoute}
        ]
      })
        .compileComponents();
    }));

    beforeEach(() => {
      fixture = TestBed.createComponent(FieldEditComponent);

    });

    it('should create', () => {
      fixture.detectChanges();

      expect(fixture.componentInstance).toBeTruthy();
    });
  });
