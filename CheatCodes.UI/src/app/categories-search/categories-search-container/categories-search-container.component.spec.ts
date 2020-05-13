import {BreakpointObserver, LayoutModule} from '@angular/cdk/layout';
import {async, ComponentFixture, TestBed} from '@angular/core/testing';
import {NoopAnimationsModule} from '@angular/platform-browser/animations';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatListModule} from '@angular/material/list';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatToolbarModule} from '@angular/material/toolbar';
import {CategoriesSearchContainerComponent} from './categories-search-container.component';
import {CategoriesSearchHttpService} from '../categories-search-http.service';
import {provideMockStore, MockStore} from '@ngrx/store/testing';
import {HttpClientTestingModule} from '@angular/common/http/testing';
import {RouterTestingModule} from '@angular/router/testing';
import {MemoizedSelector} from '@ngrx/store';
import * as fromCategorySearch from '../state';
import {CategoryBasic} from '../model/category';
import {FormsModule} from '@angular/forms';


describe('CategoriesSearchContainerComponent',
  () => {
    let fixture: ComponentFixture<CategoriesSearchContainerComponent>;
    let mockStore: MockStore;
    let mockFilteredCategoriesSelector: MemoizedSelector<fromCategorySearch.State, CategoryBasic[]>;
    const filteredCategories: CategoryBasic[] = [new CategoryBasic(), new CategoryBasic()];

    beforeEach(async(() => {
      TestBed.configureTestingModule({
        imports: [
          HttpClientTestingModule, RouterTestingModule.withRoutes([]),
          FormsModule
          , NoopAnimationsModule, LayoutModule, MatButtonModule, MatIconModule, MatListModule, MatSidenavModule, MatToolbarModule
        ],
        declarations: [CategoriesSearchContainerComponent],
        providers: [
          {provide: CategoriesSearchHttpService},
          {provide: BreakpointObserver},
          {provide: provideMockStore()}
          ]
      });
    }));

    beforeEach(() => {

      mockStore = TestBed.inject(MockStore);
      mockFilteredCategoriesSelector = mockStore.overrideSelector(
        fromCategorySearch.getFilteredCategories, filteredCategories
      );

    });

    it('should compile', () => {
      TestBed.compileComponents();
      fixture = TestBed.createComponent(CategoriesSearchContainerComponent);
      fixture.detectChanges();

      expect(fixture.componentInstance).toBeTruthy();
    });
  });
