import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {CardMoveMenuComponent} from './card-move-menu.component';
import {CategoriesService} from '../../categories/categories.service';
import {HttpClientTestingModule} from '@angular/common/http/testing';
import {RouterTestingModule} from '@angular/router/testing';
import {Category} from '../../models/category';
import {CategoryBasic} from '../../../categories-search/model/category';
import {of} from 'rxjs';
import get = Reflect.get;

describe('CardMoveMenuComponent',
  () => {
    let component: CardMoveMenuComponent;
    let fixture: ComponentFixture<CardMoveMenuComponent>;
    let currentCategories: Category[];
     let mockCategoriesService;

    beforeEach(async(() => {
      currentCategories = [
        new Category({id: 1, name: 'Test 1'}),
        new Category({id: 2, name: 'Test 2'}),
        new Category({id: 3, name: 'Test 3'})
      ];
       mockCategoriesService = jasmine.createSpyObj(['currentCategories']);

      TestBed.configureTestingModule({
        imports: [HttpClientTestingModule, RouterTestingModule.withRoutes([])],
        declarations: [CardMoveMenuComponent],
        providers: [{provide: CategoriesService}]
        // providers: [{provide: CategoriesService, useValue: mockCategoriesService}]
      })
        .compileComponents();
    }));

    beforeEach(() => {
      fixture = TestBed.createComponent(CardMoveMenuComponent);
      component = fixture.componentInstance;
      component.currentCategoryId = currentCategories[0].id;
      fixture.detectChanges();
    });

    it('should create',
      () => {
        // mockCategoriesService.currentCategories.and.returnValue(currentCategories);
        fixture.detectChanges();
        expect(component).toBeTruthy();
      });
  });
