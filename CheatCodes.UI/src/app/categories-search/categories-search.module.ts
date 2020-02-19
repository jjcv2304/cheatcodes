import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {CategoriesSearchContainerComponent} from './categories-search-container/categories-search-container.component';
import {CategoriesSearchFiltersComponent} from './categories-search-filters/categories-search-filters.component';
import {CategoriesSearchResultsComponent} from './categories-search-results/categories-search-results.component';
import {CategoriesSearchCardComponent} from './categories-search-card/categories-search-card.component';
import {CategoriesService} from '../categories/categories.service';
import {CategoriesSearchHttpService} from './categories-search-http.service';


@NgModule({
  declarations: [
    CategoriesSearchContainerComponent,
    CategoriesSearchFiltersComponent,
    CategoriesSearchResultsComponent,
    CategoriesSearchCardComponent],
  imports: [
    CommonModule
  ],
  providers: [CategoriesSearchHttpService],
})
export class CategoriesSearchModule {
}
