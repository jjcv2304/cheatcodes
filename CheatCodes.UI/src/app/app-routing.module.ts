// tslint:disable-next-line:import-spacing
import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {CategoriesListComponent} from './categories-generation/categories-list/categories-list.component';
import {CategoryEditComponent} from './categories-generation/category-edit/category-edit.component';
import {FieldEditComponent} from './categories-generation/field-edit/field-edit.component';
import {CategoriesContainerComponent} from './categories-generation/categories-container/categories-container.component';
import {SignoutRedirectCallbackComponent} from './security/signout-redirect-callback.component';
import {SigninRedirectCallbackComponent} from './security/signin-redirect-callback.component';
import {UnauthorizedComponent} from './security/unauthorized/unauthorized.component';
import {LoggedInGuard} from './guards/logged-in.guard';
import {CategoryEditGuard} from './guards/category-edit.guard';
import {CategoriesSearchContainerComponent} from './categories-search/categories-search-container/categories-search-container.component';

const appRoutes: Routes = [
  {
    path: '', component: CategoriesContainerComponent, canActivate: [LoggedInGuard],
    children: [
      {path: 'categoryList', component: CategoriesListComponent},
      {path: 'categoryList/:reload', component: CategoriesListComponent},
      {path: 'categoryEdit', component: CategoryEditComponent, canDeactivate: [CategoryEditGuard]},
      {path: 'categoryEdit/parentId/:parentId', component: CategoryEditComponent, canDeactivate: [CategoryEditGuard]},
      {path: 'categoryEdit/:parentId', component: CategoryEditComponent, canDeactivate: [CategoryEditGuard]},
      {path: 'fieldEdit/:categoryId', component: FieldEditComponent},
      {path: '', redirectTo: '/categoryList', pathMatch: 'full'},
    ]
  },
  {
    path: 'categorySearch',
    loadChildren: () => import ('./categories-search/categories-search.module').then(m => m.CategoriesSearchModule),
    canLoad: [LoggedInGuard]
  },
  {path: 'signin-callback', component: SigninRedirectCallbackComponent},
  {path: 'signout-callback', component: SignoutRedirectCallbackComponent},
  {path: 'unauthorized', component: UnauthorizedComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
