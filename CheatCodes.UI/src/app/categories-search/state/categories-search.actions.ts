import {Action} from '@ngrx/store';

export enum CategoriesSearchActionTypes {
  CategoryNameFilterAndClicked = '[Search] And Name filter',
  CategoryNameFilterOrClicked = '[Search] Or Name filter',
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

export type CategoriesSearchActions = CategoryNameFilterAndClicked | CategoryNameFilterOrClicked;
