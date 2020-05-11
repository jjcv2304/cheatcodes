import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {CategoriesListComponent} from './categories-list.component';
import {CategoriesService} from '../categories/categories.service';
import {ActivatedRoute, Router} from '@angular/router';
import {RouterTestingModule} from '@angular/router/testing';
import {HttpClientTestingModule} from '@angular/common/http/testing';

describe('CategoriesListComponent',
  () => {
    let component: CategoriesListComponent;
    let fixture: ComponentFixture<CategoriesListComponent>;
    let mockActivatedRoute;

    beforeEach(async(() => {
      mockActivatedRoute = {
        snapshot: {
          paramMap: {
            get: () => {
              return 'false';
            }
          }
        }
      };
      TestBed.configureTestingModule({
        imports: [HttpClientTestingModule,
          RouterTestingModule.withRoutes([])
        ],
        declarations: [CategoriesListComponent],
        providers: [
          {provide: CategoriesService},
          {provide: ActivatedRoute, useValue: mockActivatedRoute}
        ]
      })
        .compileComponents();
    }));

    beforeEach(() => {
      fixture = TestBed.createComponent(CategoriesListComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
    });

    it('should create',
      () => {
        expect(component).toBeTruthy();
      });
  });
