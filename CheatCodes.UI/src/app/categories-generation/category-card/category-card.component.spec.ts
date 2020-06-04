import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {CategoryCardComponent} from './category-card.component';
import {HttpClientTestingModule} from '@angular/common/http/testing';
import {RouterTestingModule} from '@angular/router/testing';
import {CategoriesService} from '../categories/categories.service';
import {ActivatedRoute} from '@angular/router';
import {Category, CategoryBuilder} from '../models/category';
import {GetRandom} from '../../test-utils/GetRandom';
import {of} from 'rxjs';
import Mock = jest.Mock;
import {createSpyObj} from '../../utils/testUtils';
import {FormsModule} from '@angular/forms';
import {CardMoveMenuComponent} from './card-move-menu/card-move-menu.component';
import {cold, getTestScheduler} from 'jasmine-marbles';

describe('CategoryCardComponent', () => {
  let fixture: ComponentFixture<CategoryCardComponent>;
  let mockCategoryService;

  beforeEach(async(() => {
    mockCategoryService = createSpyObj('CategoriesService', ['SetCategoryFilter', 'moveCategoryUp', 'moveCategoryToSibling',
      'updateCategory', 'updateCategoryField', 'deleteCategory', 'GetCurrentCategories']);
    TestBed.configureTestingModule({
      imports: [FormsModule, HttpClientTestingModule, RouterTestingModule.withRoutes([])],
      declarations: [CategoryCardComponent, CardMoveMenuComponent],
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
    mockCategoryService.moveCategoryUp.mockReturnValue(of(true));
    fixture = TestBed.createComponent(CategoryCardComponent);
    fixture.componentInstance.card = CategoryBuilder.basic().setParentId(GetRandom.Number()).build();
    fixture.detectChanges();

    fixture.componentInstance.moveCardUp();

    expect(mockCategoryService.moveCategoryUp).toHaveBeenCalledTimes(1);
  });
  it('moveCardToSibling should call moveCardToSibling', () => {
    mockCategoryService.moveCategoryToSibling.mockReturnValue(of(true));
    fixture = TestBed.createComponent(CategoryCardComponent);
    fixture.componentInstance.card = CategoryBuilder.basic().setParentId(GetRandom.Number()).build();
    fixture.detectChanges();

    fixture.componentInstance.moveCardToSibling(GetRandom.Number());

    expect(mockCategoryService.moveCategoryToSibling).toHaveBeenCalledTimes(1);
  });
  it('saveCard should set saveIsRecommended flag to false', () => {
    fixture = TestBed.createComponent(CategoryCardComponent);
    fixture.componentInstance.card = CategoryBuilder.basic().build();
    fixture.detectChanges();
    fixture.componentInstance.saveIsRecommended = true;
    // marble setup
    const categoriesService = TestBed.inject(CategoriesService);
    const expected$ = cold('a|');
    categoriesService.updateCategory = jest.fn(() => expected$);

    fixture.componentInstance.saveCard();
    getTestScheduler().flush();

    expect(fixture.componentInstance.saveIsRecommended).toEqual(false);
  });

});
