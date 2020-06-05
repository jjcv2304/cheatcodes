import * as fromReducer from './categories-search.reducer';
import * as fromActions from './categories-search.actions';
import {CategoriesSearchState} from './categories-search.reducer';

describe('search reducer', () => {

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

  it('should return the default state', () => {
    const state = fromReducer.reducer(undefined, undefined);

    // @ts-ignore
    expect(state).toStrictEqual(initialState);
  });
  it('should set categoryDescriptionFilterOr to payload', () => {
    const payload = true;
    const action = new fromActions.CategoryNameFilterOrClicked(payload);
    const state = fromReducer.reducer(initialState, action);

    expect(state.categoryNameFilterOr).toEqual(payload);
  });
  it('should set categoryDescriptionFilterOr to payload', () => {
    const payload = false;
    const action = new fromActions.CategoryNameFilterOrClicked(payload);
    const state = fromReducer.reducer(initialState, action);

    expect(state.categoryNameFilterOr).toEqual(payload);
  });
  it('should set categoryDescriptionFilterOr to payload', () => {
    const payload = 'Angular';
    const action = new fromActions.CategoryNameFilterEnter(payload);
    const state = fromReducer.reducer(initialState, action);

    expect(state.categoryNameFilter).toEqual(payload);
  });
});
