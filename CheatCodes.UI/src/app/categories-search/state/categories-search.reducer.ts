export function reducer(state, action) {
  switch (action.type) {

    case 'CategorySearchFilters.categoryNameFilterOrClicked':
      return {...state, categoryNameFilterOr: action.payload, categoryNameFilterAnd: false};

    case 'CategorySearchFilters.categoryNameFilterAndClicked':
      return {...state, categoryNameFilterAnd: action.payload, categoryNameFilterOr: false};

    default:
      return state;
  }
}
