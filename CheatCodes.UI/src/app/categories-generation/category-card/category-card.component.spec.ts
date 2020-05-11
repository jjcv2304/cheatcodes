import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {CategoryCardComponent} from './category-card.component';
import {HttpClientTestingModule} from '@angular/common/http/testing';
import {RouterTestingModule} from '@angular/router/testing';
import {CategoriesService} from '../categories/categories.service';
import {ActivatedRoute} from '@angular/router';
import {Category} from '../models/category';

describe('CategoryCardComponent',
  () => {
    let component: CategoryCardComponent;
    let fixture: ComponentFixture<CategoryCardComponent>;

    beforeEach(async(() => {
      TestBed.configureTestingModule({
        imports: [HttpClientTestingModule, RouterTestingModule.withRoutes([])],
        declarations: [CategoryCardComponent],
        providers: [{provide: CategoriesService}]
      })
        .compileComponents();
    }));

    beforeEach(() => {
      fixture = TestBed.createComponent(CategoryCardComponent);
      component = fixture.componentInstance;
      const expectedCard = new Category();
      component.card = expectedCard;
      fixture.detectChanges();
    });

    it('should create',
      () => {
        expect(component).toBeTruthy();
      });
  });
