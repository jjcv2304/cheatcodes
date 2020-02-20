import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {CategoriesSearchContainerComponent} from './categories-search-container/categories-search-container.component';
import {CategoriesSearchFiltersComponent} from './categories-search-filters/categories-search-filters.component';
import {CategoriesSearchResultsComponent} from './categories-search-results/categories-search-results.component';
import {CategoriesSearchCardComponent} from './categories-search-card/categories-search-card.component';
import {CategoriesSearchHttpService} from './categories-search-http.service';
import {MatSliderModule} from '@angular/material/slider';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import {RouterModule} from '@angular/router';


@NgModule({
  declarations: [
    CategoriesSearchContainerComponent,
    CategoriesSearchFiltersComponent,
    CategoriesSearchResultsComponent,
    CategoriesSearchCardComponent],
  imports: [
    CommonModule,
    MatSliderModule,
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    RouterModule.forChild([])
  ],
  providers: [CategoriesSearchHttpService],
})
export class CategoriesSearchModule {
}
