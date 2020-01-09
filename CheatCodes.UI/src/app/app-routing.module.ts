// tslint:disable-next-line:import-spacing
import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {CategoriesListComponent} from './categories-list/categories-list.component';
import {CategoryEditComponent} from './category-edit/category-edit.component';


const routes: Routes = [
  { path: 'categoryList', component: CategoriesListComponent },
  { path: 'categoryEdit', component: CategoryEditComponent },
  { path: 'categoryEdit/:parentId', component: CategoryEditComponent },
  { path: '', redirectTo: '/categoryList', pathMatch: 'full' },
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
