import {Action} from '@ngrx/store';
import {CategoryBasic} from '../model/category';
import {CategorySearchFilters} from '../model/categorySearchFilters';

export enum CategoriesSearchActionTypes {
  CategoryNameFilterAndClicked = '[Search] And Name filter',
  CategoryNameFilterOrClicked = '[Search] Or Name filter',
  CategoryNameFilterEnter = '[Search] Name filter',
  CategoryName2FilterEnter = '[Search] Name filter 2',
  CategoryDescriptionFilterAndClicked = '[Search] And Description filter',
  CategoryDescriptionFilterOrClicked = '[Search] Or Description filter',
  CategoryDescriptionFilterEnter = '[Search] Description filter ',
  ShowButtonShowSideNav = '[Search] Show button to show side nav panel',
  CategoriesFilter = '[Search] Categories filter',
  CategoriesFilterSuccess = '[Search] Categories filter success',
  CategoriesFilterFail = '[Search] Categories filter fail'
}

export class CategoryNameFilterAndClicked implements Action {
  readonly type = CategoriesSearchActionTypes.CategoryNameFilterAndClicked;

  constructor(public payload: boolean) {
  }
}

export class CategoryNameFilterOrClicked implements Action {
  readonly type = CategoriesSearchActionTypes.CategoryNameFilterOrClicked;

  constructor(public payload: boolean) {
  }
}

export class CategoryNameFilterEnter implements Action {
  readonly type = CategoriesSearchActionTypes.CategoryNameFilterEnter;

  constructor(public payload: string) {
  }
}

export class CategoryName2FilterEnter implements Action {
  readonly type = CategoriesSearchActionTypes.CategoryName2FilterEnter;

  constructor(public payload: string) {
  }
}

export class CategoryDescriptionFilterAndClicked implements Action {
  readonly type = CategoriesSearchActionTypes.CategoryDescriptionFilterAndClicked;

  constructor(public payload: boolean) {
  }
}

export class CategoryDescriptionFilterOrClicked implements Action {
  readonly type = CategoriesSearchActionTypes.CategoryDescriptionFilterOrClicked;

  constructor(public payload: boolean) {
  }
}

export class CategoryDescriptionFilterEnter implements Action {
  readonly type = CategoriesSearchActionTypes.CategoryDescriptionFilterEnter;

  constructor(public payload: string) {
  }
}

export class ShowButtonShowSideNav implements Action {
  readonly type = CategoriesSearchActionTypes.ShowButtonShowSideNav;

  constructor(public payload: boolean) {
  }
}

export class CategoriesFilter implements Action {
  readonly type = CategoriesSearchActionTypes.CategoriesFilter;

  constructor(public payload: CategorySearchFilters) {  }
}

export class CategoriesFilterSuccess implements Action {
  readonly type = CategoriesSearchActionTypes.CategoriesFilterSuccess;

  constructor(public payload: CategoryBasic[]) {  }
}

export class CategoriesFilterFail implements Action {
  readonly type = CategoriesSearchActionTypes.CategoriesFilterFail;

  constructor(public payload: string) {  }
}

export type CategoriesSearchActions =
  CategoryNameFilterAndClicked
  | CategoryNameFilterOrClicked
  | CategoryNameFilterEnter
  | CategoryName2FilterEnter
  | CategoryDescriptionFilterAndClicked
  | CategoryDescriptionFilterOrClicked
  | CategoryDescriptionFilterEnter
  | ShowButtonShowSideNav
  | CategoriesFilter
  | CategoriesFilterSuccess
  | CategoriesFilterFail;
