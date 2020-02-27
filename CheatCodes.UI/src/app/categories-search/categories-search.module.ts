import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {CategoriesSearchContainerComponent} from './categories-search-container/categories-search-container.component';
import {CategoriesSearchFiltersComponent} from './categories-search-filters/categories-search-filters.component';

import {CategoriesSearchHttpService} from './categories-search-http.service';
import {MatSliderModule} from '@angular/material/slider';
import {LayoutModule} from '@angular/cdk/layout';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatButtonModule} from '@angular/material/button';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatIconModule} from '@angular/material/icon';
import {MatListModule} from '@angular/material/list';
import {RouterModule} from '@angular/router';
import {MatFormFieldModule, MatInputModule} from '@angular/material';
import {ReactiveFormsModule} from '@angular/forms';
import { CategoriesSearchListResultComponent } from './categories-search-list-result/categories-search-list-result.component';
import { CategoriesSearchTreeDetailResultComponent } from './categories-search-tree-detail-result/categories-search-tree-detail-result.component';
import { CardResultBasicComponent } from './card-result-basic/card-result-basic.component';
import { CardResultDetailComponent } from './card-result-detail/card-result-detail.component';
import {MatCardModule} from '@angular/material/card';
import {MatTreeModule} from '@angular/material/tree';

@NgModule({
  declarations: [
    CategoriesSearchContainerComponent,
    CategoriesSearchFiltersComponent,
    CategoriesSearchListResultComponent,
    CategoriesSearchTreeDetailResultComponent,
    CardResultBasicComponent,
    CardResultDetailComponent],
  imports: [
    CommonModule,
    MatSliderModule,
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    RouterModule.forChild([]),
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    MatCardModule,
    MatTreeModule

  ],
  providers: [CategoriesSearchHttpService],
})
export class CategoriesSearchModule {
}
