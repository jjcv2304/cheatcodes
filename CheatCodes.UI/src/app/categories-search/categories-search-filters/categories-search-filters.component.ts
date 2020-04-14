import {AfterViewInit, Component, OnInit} from '@angular/core';
import {FormControl, FormGroup} from '@angular/forms';
import {BreakpointObserver} from '@angular/cdk/layout';
import {CategoriesSearchHttpService} from '../categories-search-http.service';
import {CategorySearchFilters} from '../model/categorySearchFilters';
import {select, Store} from '@ngrx/store';

@Component({
  selector: 'app-categories-search-filters',
  templateUrl: './categories-search-filters.component.html',
  styleUrls: ['./categories-search-filters.component.scss']
})
export class CategoriesSearchFiltersComponent implements AfterViewInit, OnInit {

  searchCategoriesForm = new FormGroup({
    categoryNameFilter: new FormControl(''),
    categoryName2Filter: new FormControl('')
  });

  categoryNameFilterAnd = false;
  categoryNameFilterOr = false;

  constructor(private breakpointObserver: BreakpointObserver, private apiService: CategoriesSearchHttpService, private store: Store<any>) {
  }

  ngOnInit(): void {
    this.searchCategoriesForm.get('categoryName2Filter').disable();

    // TODO: unsubscribe
    this.store.pipe(select('categorySearchReducer')).subscribe(
      categorySearchReducer => {
        if (categorySearchReducer) {
          this.categoryNameFilterOr = categorySearchReducer.categoryNameFilterOr;
          this.categoryNameFilterAnd = categorySearchReducer.categoryNameFilterAnd;
        }
      }
    );
  }

  ngAfterViewInit(): void {

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
      type: 'CategorySearchFilters.categoryNameFilterAndClicked',
      payload: $event.checked
    });
  }

  categoryNameFilterOrClicked($event) {
    this.store.dispatch({
      type: 'CategorySearchFilters.categoryNameFilterOrClicked',
      payload: $event.checked
    });
  }


}
