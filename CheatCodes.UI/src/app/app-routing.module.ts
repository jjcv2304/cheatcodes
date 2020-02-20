// tslint:disable-next-line:import-spacing
import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {CategoriesListComponent} from './categories-generation/categories-list/categories-list.component';
import {CategoryEditComponent} from './categories-generation/category-edit/category-edit.component';
import {FieldEditComponent} from './categories-generation/field-edit/field-edit.component';
import {CategoriesSearchContainerComponent} from './categories-search/categories-search-container/categories-search-container.component';
import {CategoriesContainerComponent} from './categories-generation/categories-container/categories-container.component';

const appRoutes: Routes = [
  {
    path: '',
    component: CategoriesContainerComponent,
    children: [
      { path: 'categoryList', component: CategoriesListComponent },
      { path: 'categoryList/:reload', component: CategoriesListComponent },
      { path: 'categoryEdit', component: CategoryEditComponent },
      { path: 'categoryEdit/parentId/:parentId', component: CategoryEditComponent },
      { path: 'categoryEdit/:parentId', component: CategoryEditComponent },
      { path: 'fieldEdit/:categoryId', component: FieldEditComponent },
      { path: '', redirectTo: '/categoryList', pathMatch: 'full' },
    ]
  },
  { path: 'categorySearch', component: CategoriesSearchContainerComponent },
];

@NgModule({
  imports: [ RouterModule.forRoot(appRoutes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
