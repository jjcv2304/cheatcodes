import {CategoriesSearchActions, CategoriesSearchActionTypes} from './categories-search.actions';
import {CategoryBasic} from '../model/category';

export interface CategoriesSearchState {
  categoryNameFilterOr: boolean;
  categoryNameFilterAnd: boolean;
  categoryNameFilter: string;
  categoryName2Filter: string;
  categoryDescriptionFilterOr: boolean;
  categoryDescriptionFilterAnd: boolean;
  categoryDescriptionFilter: string;
  showButtonShowSideNav: boolean;
  filteredCategories: CategoryBasic[];
  error: string;
}

const initialState: CategoriesSearchState = {
  categoryNameFilterAnd: false,
  categoryNameFilterOr: false,
  categoryNameFilter: '',
  categoryName2Filter: '',
  categoryDescriptionFilterAnd: false,
  categoryDescriptionFilterOr: false,
  categoryDescriptionFilter: '',
  showButtonShowSideNav: false,
  filteredCategories: [],
  error: ''
};


export function reducer(state = initialState, action: CategoriesSearchActions): CategoriesSearchState {
  if (action === undefined) {
    return state;
  }
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

    case CategoriesSearchActionTypes.CategoryDescriptionFilterOrClicked:
      return {
        ...state,
        categoryDescriptionFilterOr: action.payload,
        categoryDescriptionFilterAnd: false
      };

    case CategoriesSearchActionTypes.CategoryDescriptionFilterAndClicked:
      return {
        ...state,
        categoryDescriptionFilterAnd: action.payload,
        categoryDescriptionFilterOr: false
      };

    case CategoriesSearchActionTypes.CategoryDescriptionFilterEnter:
      return {
        ...state,
        categoryDescriptionFilter: action.payload,
      };

    case CategoriesSearchActionTypes.ShowButtonShowSideNav:
      return {
        ...state,
        showButtonShowSideNav: action.payload,
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
