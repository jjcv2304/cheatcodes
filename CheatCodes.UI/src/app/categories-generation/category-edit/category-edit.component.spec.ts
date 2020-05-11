import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {CategoryEditComponent} from './category-edit.component';
import {HttpClientTestingModule} from '@angular/common/http/testing';
import {RouterTestingModule} from '@angular/router/testing';
import {CategoriesService} from '../categories/categories.service';
import {ActivatedRoute} from '@angular/router';
import {FormsModule, NgForm} from '@angular/forms';

describe('CategoryEditComponent',
  () => {
    let component: CategoryEditComponent;
    let fixture: ComponentFixture<CategoryEditComponent>;
    let mockActivatedRoute;

    beforeEach(async(() => {
      mockActivatedRoute = {
        snapshot: {
          paramMap: {
            get: () => {
              return 1;
            }
          }
        }
      };
      TestBed.configureTestingModule({
        imports: [FormsModule,
          HttpClientTestingModule,
          RouterTestingModule.withRoutes([]),
        ],
        declarations: [CategoryEditComponent],
        providers: [
          {provide: CategoriesService},
          {provide: ActivatedRoute, useValue: mockActivatedRoute}
        ]
      })
        .compileComponents();
    }));

    beforeEach(() => {
      fixture = TestBed.createComponent(CategoryEditComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
    });

    it('should create',
      () => {
        expect(component).toBeTruthy();
      });
  });
