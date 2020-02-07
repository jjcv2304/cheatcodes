// tslint:disable-next-line:import-spacing
import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {CategoriesListComponent} from './categories-list/categories-list.component';
import {CategoryEditComponent} from './category-edit/category-edit.component';
import {FieldEditComponent} from './field-edit/field-edit.component';


const routes: Routes = [
  { path: 'categoryList', component: CategoriesListComponent },
  { path: 'categoryList/:reload', component: CategoriesListComponent },
  { path: 'categoryEdit', component: CategoryEditComponent },
  { path: 'categoryEdit/parentId/:parentId', component: CategoryEditComponent },
  { path: 'categoryEdit/:parentId', component: CategoryEditComponent },
  { path: 'fieldEdit/:categoryId', component: FieldEditComponent },
  { path: '', redirectTo: '/categoryList', pathMatch: 'full' },
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
