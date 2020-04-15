import * as fromRoot from '../../state/app.state';
import {createFeatureSelector, createSelector} from '@ngrx/store';
import {CategoriesSearchActions, CategoriesSearchActionTypes} from './categories-search.actions';
import {CategoryBasic} from '../model/category';

export interface State extends fromRoot.State {
  categoriesSearch: CategoriesSearchState;
}

export interface CategoriesSearchState {
  categoryNameFilterOr: boolean;
  categoryNameFilterAnd: boolean;
  categoryNameFilter: string;
  categoryName2Filter: string;
  filteredCategories: CategoryBasic[];
  error: string;
}

const initialState: CategoriesSearchState = {
  categoryNameFilterAnd: false,
  categoryNameFilterOr: false,
  categoryNameFilter: '',
  categoryName2Filter: '',
  filteredCategories: [],
  error: ''
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
export const getFilteredCategories = createSelector(
  getCategoriesSearchFeatureState,
  state => state.filteredCategories
);
export const getCategoryNameFilter = createSelector(
  getCategoriesSearchFeatureState,
  state => state.categoryNameFilter
);

export const getCategoryName2Filter = createSelector(
  getCategoriesSearchFeatureState,
  state => state.categoryName2Filter
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

    case CategoriesSearchActionTypes.CategoryNameFilterEnter:
      return {
        ...state,
        categoryNameFilter: action.payload,
      };

    case CategoriesSearchActionTypes.CategoryName2FilterEnter:
      return {
        ...state,
        categoryName2Filter: action.payload,
      };

    case CategoriesSearchActionTypes.CategoriesFilterSuccess:
      return {
        ...state,
        filteredCategories: action.payload,
        error: ''
      };
    case CategoriesSearchActionTypes.CategoriesFilterFail:
      return {
        ...state,
        filteredCategories: [],
        error: action.payload
      };

    default:
      return state;
  }
}
