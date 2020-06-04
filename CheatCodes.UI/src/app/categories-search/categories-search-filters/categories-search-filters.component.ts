import {Component, EventEmitter, OnDestroy, OnInit, Output} from '@angular/core';
import {FormControl, FormGroup} from '@angular/forms';
import {BreakpointObserver} from '@angular/cdk/layout';
import {CategoriesSearchHttpService} from '../categories-search-http.service';
import {CategorySearchFilters} from '../model/categorySearchFilters';
import {select, Store} from '@ngrx/store';
import * as fromCategorySearch from '../state';
import * as searchActions from './../state/categories-search.actions';
import {takeWhile} from 'rxjs/operators';

@Component({
  selector: 'app-categories-search-filters',
  templateUrl: './categories-search-filters.component.html',
  styleUrls: ['./categories-search-filters.component.scss']
})
export class CategoriesSearchFiltersComponent implements OnInit, OnDestroy {

  searchCategoriesForm = new FormGroup({
    categoryNameFilter: new FormControl(''),
    categoryName2Filter: new FormControl(''),
    categoryDescriptionFilter: new FormControl('')
  });
  categoryNameFilterOr: boolean;
  categoryNameFilterAnd: boolean;
  categoryDescriptionFilterOr: boolean;
  categoryDescriptionFilterAnd: boolean;
  private componentActive = true;
  @Output() valueChange: EventEmitter<void> = new EventEmitter();

  constructor(private breakpointObserver: BreakpointObserver,
              private apiService: CategoriesSearchHttpService,
              private store: Store<fromCategorySearch.State>) {
  }

  ngOnInit(): void {
    this.searchCategoriesForm.get('categoryName2Filter').disable();
    this.searchCategoriesForm.get('categoryDescriptionFilter').disable();

    this.store.pipe(select(fromCategorySearch.getCategoryNameFilterAnd), takeWhile(() => this.componentActive)).subscribe(
      categoryNameFilterAnd => {
        this.categoryNameFilterAnd = categoryNameFilterAnd;
        if (this.categoryNameFilterAnd) {
          this.searchCategoriesForm.get('categoryName2Filter').enable();
        }
      }
    );
    this.store.pipe(select(fromCategorySearch.getCategoryNameFilterOr), takeWhile(() => this.componentActive)).subscribe(
      categoryNameFilterOr => {
        this.categoryNameFilterOr = categoryNameFilterOr;
        if (this.categoryNameFilterOr) {
          this.searchCategoriesForm.get('categoryName2Filter').enable();
        }
      }
    );
    this.store.pipe(select(fromCategorySearch.getCategoryNameFilter), takeWhile(() => this.componentActive)).subscribe(
      categoryNameFilter => {
        this.searchCategoriesForm.get('categoryNameFilter').setValue(categoryNameFilter);
      }
    );
    this.store.pipe(select(fromCategorySearch.getCategoryName2Filter), takeWhile(() => this.componentActive)).subscribe(
      categoryName2Filter => {
        this.searchCategoriesForm.get('categoryName2Filter').setValue(categoryName2Filter);
      }
    );
    this.store.pipe(select(fromCategorySearch.getCategoryDescriptionFilterOr), takeWhile(() => this.componentActive)).subscribe(
      categoryDescriptionFilterOr => {
        this.categoryDescriptionFilterOr = categoryDescriptionFilterOr;
        if (this.categoryDescriptionFilterOr) {
          this.searchCategoriesForm.get('categoryDescriptionFilter').enable();
        }
      }
    );
    this.store.pipe(select(fromCategorySearch.getCategoryDescriptionFilterAnd), takeWhile(() => this.componentActive)).subscribe(
      categoryDescriptionFilterAnd => {
        this.categoryDescriptionFilterAnd = categoryDescriptionFilterAnd;
        if (this.categoryDescriptionFilterAnd) {
          this.searchCategoriesForm.get('categoryDescriptionFilter').enable();
        }
      }
    );
    this.store.pipe(select(fromCategorySearch.getCategoryDescriptionFilter), takeWhile(() => this.componentActive)).subscribe(
      categoryDescriptionFilter => {
        this.searchCategoriesForm.get('categoryDescriptionFilter').setValue(categoryDescriptionFilter);
      }
    );
  }

  Search() {
    const filters = new CategorySearchFilters();
    filters.categoryNameFilter = this.searchCategoriesForm.get('categoryNameFilter').value;
    filters.categoryNameFilterAnd = this.categoryNameFilterAnd;
    filters.categoryNameFilterOr = this.categoryNameFilterOr;
    if (this.categoryNameFilterAnd || this.categoryNameFilterOr) {
      filters.categoryName2Filter = this.searchCategoriesForm.get('categoryName2Filter').value;
    }
    filters.categoryDescriptionFilterAnd = this.categoryDescriptionFilterAnd;
    filters.categoryDescriptionFilterOr = this.categoryDescriptionFilterOr;
    if (this.categoryDescriptionFilterAnd || this.categoryDescriptionFilterOr) {
      filters.categoryDescriptionFilter = this.searchCategoriesForm.get('categoryDescriptionFilter').value;
    }

    this.valueChange.emit();
    this.store.dispatch(new searchActions.CategoriesFilter(filters));
  }

  categoryNameFilterAndClicked($event) {
    this.store.dispatch(new searchActions.CategoryNameFilterAndClicked($event.checked));
  }

  categoryNameFilterOrClicked($event) {
    this.store.dispatch(new searchActions.CategoryNameFilterOrClicked($event.checked));
  }

  categoryDescriptionFilterAndClicked($event) {
    this.store.dispatch(new searchActions.CategoryDescriptionFilterAndClicked($event.checked));
  }

  categoryDescriptionFilterOrClicked($event) {
    this.store.dispatch(new searchActions.CategoryDescriptionFilterOrClicked($event.checked));
  }

  categoryNameFilterChanged(value: string) {
    this.store.dispatch(new searchActions.CategoryNameFilterEnter(value));
  }

  categoryNames2FilterChanged(value: string) {
    this.store.dispatch(new searchActions.CategoryName2FilterEnter(value));
  }

  categoryDescriptionFilterChanged(value: string) {
    this.store.dispatch(new searchActions.CategoryDescriptionFilterEnter(value));
  }

  ngOnDestroy(): void {
    this.componentActive = false;
  }
}
