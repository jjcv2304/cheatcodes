import {async, ComponentFixture, TestBed} from '@angular/core/testing';
import {CategoriesListComponent} from './categories-list.component';
import {CategoriesService} from '../categories/categories.service';
import {ActivatedRoute, Router} from '@angular/router';
import {RouterTestingModule} from '@angular/router/testing';
import {HttpClientTestingModule} from '@angular/common/http/testing';
import {CategoryBuilder} from '../models/category';
import {CategoryCardComponent} from '../category-card/category-card.component';
import {By} from '@angular/platform-browser';
import {createSpyObj} from '../../test-utils/testUtils';
import {FormsModule} from '@angular/forms';
import {CardMoveMenuComponent} from '../category-card/card-move-menu/card-move-menu.component';

describe('CategoriesListComponent',
  () => {
    let fixture: ComponentFixture<CategoriesListComponent>;
    let mockActivatedRoute;
    let mockCategoryService;
    let CATEGORIES;

    beforeEach(async(() => {
      mockActivatedRoute = {snapshot: {paramMap: {get: () => false}}};
      mockCategoryService = createSpyObj('CategoriesService', ['SetCategoryFilter', 'RefreshCategoryLastFilter', 'GetCurrentCategories']);
      CATEGORIES = [
        new CategoryBuilder().simple().build(),
        new CategoryBuilder().simple().build(),
        new CategoryBuilder().simple().build()
      ];
      TestBed.configureTestingModule({
        imports: [FormsModule, HttpClientTestingModule, RouterTestingModule.withRoutes([])],
        declarations: [CategoriesListComponent, CategoryCardComponent, CardMoveMenuComponent],
        providers: [
          {provide: CategoriesService, useValue: mockCategoryService},
          {provide: ActivatedRoute, useValue: mockActivatedRoute}
        ]
      });
    }));

    it('should call RefreshCategoryLastFilter when reload is true', () => {
      const mockActivatedRouteOverride = {snapshot: {paramMap: {get: () => true}}};
      TestBed.overrideProvider(ActivatedRoute, {useValue: mockActivatedRouteOverride}).compileComponents();

      fixture = TestBed.createComponent(CategoriesListComponent);

      fixture.detectChanges();

      expect(mockCategoryService.RefreshCategoryLastFilter).toHaveBeenCalledTimes(1);
    });
    it('should call SetCategoryFilter when reload is false', () => {
      const mockActivatedRouteOverride = {snapshot: {paramMap: {get: () => false}}};
      TestBed.overrideProvider(ActivatedRoute, {useValue: mockActivatedRouteOverride}).compileComponents();
      fixture = TestBed.createComponent(CategoriesListComponent);

      fixture.detectChanges();

      expect(mockCategoryService.SetCategoryFilter).toHaveBeenCalledTimes(1);
    });
    it('should generate 3 cards when 3 cards are returned', () => {
       mockCategoryService.GetCurrentCategories.mockReturnValue(CATEGORIES);
       TestBed.compileComponents();
       fixture = TestBed.createComponent(CategoriesListComponent);

       fixture.detectChanges();

       const cardDEs = fixture.debugElement.queryAll(By.directive(CategoryCardComponent));
       expect(cardDEs.length).toEqual(3);
      for (let i = 0; i < cardDEs.length; i++) {
        expect(cardDEs[i].componentInstance.card).toEqual(CATEGORIES[i]);
      }
    });

  });
