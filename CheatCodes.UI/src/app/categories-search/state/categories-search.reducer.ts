import * as fromRoot from '../../state/app.state';
import {createFeatureSelector, createSelector} from '@ngrx/store';
import {CategoriesSearchActions, CategoriesSearchActionTypes} from './categories-search.actions';

export interface State extends fromRoot.State {
  categoriesSearch: CategoriesSearchState;
}

export interface CategoriesSearchState {
  categoryNameFilterOr: boolean;
  categoryNameFilterAnd: boolean;
}

const initialState: CategoriesSearchState = {
  categoryNameFilterAnd: false,
  categoryNameFilterOr: false
};

const getCategoriesSearchFeatureState = createFeatureSelector<CategoriesSearchState>('categoriesSearchReducer');

export const getCategoryNameFilterAnd = createSelector(
  getCategoriesSearchFeatureState,
  state => state.categoryNameFilterAnd
);
export const getCategoryNameFilterOr = createSelector(
  getCategoriesSearchFeatureState,
  state => state.categoryNameFilterOr
);

export function reducer(state = initialState, action: CategoriesSearchActions): CategoriesSearchState {
  switch (action.type) {

    case CategoriesSearchActionTypes.CategoryNameFilterOrClicked:
      return {
        ...state,
        categoryNameFilterOr: action.payload,
        categoryNameFilterAnd: false
      };

    case CategoriesSearchActionTypes.CategoryNameFilterAndClicked:
      return {
        ...state,
        categoryNameFilterAnd: action.payload,
        categoryNameFilterOr: false
      };

    default:
      return state;
  }
}
