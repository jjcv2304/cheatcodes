import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {CategoryCardComponent} from './category-card.component';
import {HttpClientTestingModule} from '@angular/common/http/testing';
import {RouterTestingModule} from '@angular/router/testing';
import {CategoriesService} from '../categories/categories.service';
import {ActivatedRoute} from '@angular/router';
import {Category, CategoryBuilder} from '../models/category';
import {GetRandom} from '../../test-utils/GetRandom';
import {of} from 'rxjs';

describe('CategoryCardComponent',
  () => {
    let fixture: ComponentFixture<CategoryCardComponent>;
    let mockCategoryService;

    beforeEach(async(() => {
      mockCategoryService = jasmine.createSpyObj(['SetCategoryFilter', 'moveCategoryUp', 'moveCategoryToSibling', 'updateCategory', 'updateCategoryField', 'deleteCategory', 'GetCurrentCategories']);
      TestBed.configureTestingModule({
        imports: [HttpClientTestingModule, RouterTestingModule.withRoutes([])],
        declarations: [CategoryCardComponent],
        providers: [{provide: CategoriesService, useValue: mockCategoryService}]
      }).compileComponents();
    }));

    it('should create', () => {
      fixture = TestBed.createComponent(CategoryCardComponent);
      fixture.componentInstance.card = CategoryBuilder.basic().build();
      fixture.detectChanges();

      expect(fixture.componentInstance).toBeTruthy();
    });

    it('canNavigate should is true when card hasParent', () => {
      fixture = TestBed.createComponent(CategoryCardComponent);
      fixture.componentInstance.card = CategoryBuilder.basic().setHasParent(true).build();
      fixture.detectChanges();

      const canNavigate = fixture.componentInstance.canNavigateUp();

      expect(canNavigate).toEqual(true);
    });
    it('canNavigate should is false when card hasParent is false', () => {
      fixture = TestBed.createComponent(CategoryCardComponent);
      fixture.componentInstance.card = CategoryBuilder.basic().setHasParent(false).build();
      fixture.detectChanges();

      const canNavigate = fixture.componentInstance.canNavigateUp();

      expect(canNavigate).toEqual(false);
    });
    it('navigateUp should call SetCategoryFilter', () => {
      fixture = TestBed.createComponent(CategoryCardComponent);
      fixture.componentInstance.card = CategoryBuilder.basic().setParentId(GetRandom.Number()).build();
      fixture.detectChanges();

      fixture.componentInstance.navigateUp();

      expect(mockCategoryService.SetCategoryFilter).toHaveBeenCalledTimes(1);
    });
    it('moveCardUp should call moveCardUp', () => {
      mockCategoryService.moveCategoryUp.and.returnValue(of(true));
      fixture = TestBed.createComponent(CategoryCardComponent);
      fixture.componentInstance.card = CategoryBuilder.basic().setParentId(GetRandom.Number()).build();
      fixture.detectChanges();

      fixture.componentInstance.moveCardUp();

      expect(mockCategoryService.moveCategoryUp).toHaveBeenCalledTimes(1);
    });
    it('moveCardToSibling should call moveCardToSibling', () => {
      mockCategoryService.moveCategoryToSibling.and.returnValue(of(true));
      fixture = TestBed.createComponent(CategoryCardComponent);
      fixture.componentInstance.card = CategoryBuilder.basic().setParentId(GetRandom.Number()).build();
      fixture.detectChanges();

      fixture.componentInstance.moveCardToSibling(GetRandom.Number());

      expect(mockCategoryService.moveCategoryToSibling).toHaveBeenCalledTimes(1);
    });

  });
