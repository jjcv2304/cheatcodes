import {AfterViewInit, Component, OnInit} from '@angular/core';
import {FormControl, FormGroup} from '@angular/forms';
import {BreakpointObserver} from '@angular/cdk/layout';
import {CategoriesSearchHttpService} from '../categories-search-http.service';
import {CategorySearchFilters} from '../model/categorySearchFilters';
import {select, Store} from '@ngrx/store';
import * as fromCategorySearch from '../state/categories-search.reducer';

@Component({
  selector: 'app-categories-search-filters',
  templateUrl: './categories-search-filters.component.html',
  styleUrls: ['./categories-search-filters.component.scss']
})
export class CategoriesSearchFiltersComponent implements OnInit {

  searchCategoriesForm = new FormGroup({
    categoryNameFilter: new FormControl(''),
    categoryName2Filter: new FormControl('')
  });
  private categoryNameFilterOr: boolean;
  private categoryNameFilterAnd: boolean;

  constructor(private breakpointObserver: BreakpointObserver,
              private apiService: CategoriesSearchHttpService,
              private store: Store<fromCategorySearch.State>) {
  }

  ngOnInit(): void {
    this.searchCategoriesForm.get('categoryName2Filter').disable();

    // TODO: unsubscribe
    this.store.pipe(select(fromCategorySearch.getCategoryNameFilterAnd)).subscribe(
      categoryNameFilterAnd => this.categoryNameFilterAnd = categoryNameFilterAnd
    );
    this.store.pipe(select(fromCategorySearch.getCategoryNameFilterOr)).subscribe(
      categoryNameFilterOr => this.categoryNameFilterOr = categoryNameFilterOr
    );
  }

  Search() {
    const filters = new CategorySearchFilters();
    filters.categoryNameFilter = this.searchCategoriesForm.get('categoryNameFilter').value;
    if (this.categoryNameFilterAnd || this.categoryNameFilterOr) {
      filters.categoryNameFilterAnd = this.categoryNameFilterAnd;
      filters.categoryNameFilterOr = this.categoryNameFilterOr;
      filters.categoryName2Filter = this.searchCategoriesForm.get('categoryName2Filter').value;
    }
    this.apiService.GetCategoriesByFiltersAsync(filters);
  }

  categoryNameFilterAndClicked($event) {
    this.store.dispatch({
      type: 'categoryNameFilterAndClicked',
      payload: $event.checked
    });
  }

  categoryNameFilterOrClicked($event) {
    this.store.dispatch({
      type: 'categoryNameFilterOrClicked',
      payload: $event.checked
    });
  }


}
