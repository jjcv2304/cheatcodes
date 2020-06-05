import {TestBed} from '@angular/core/testing';
import {provideMockActions} from '@ngrx/effects/testing';
import {cold, hot} from 'jasmine-marbles';
import {Observable, throwError} from 'rxjs';
import {CategoriesSearchEffects} from './categories-search.effects';
import {createSpyObj} from '../../test-utils/testUtils';
import {CategoryBasic, CategoryBasicBuilder, ICategoryBasic} from '../model/category';
import {CategoriesFilter, CategoriesFilterFail, CategoriesFilterSuccess} from './categories-search.actions';
import {CategorySearchFilters} from '../model/categorySearchFilters';
import {HttpClientTestingModule} from '@angular/common/http/testing';
import {CategoriesSearchHttpService} from '../categories-search-http.service';
import {Envelope} from '../../utils/envelope';
import * as searchActions from './categories-search.actions';

describe('CategoriesSearchEffects', () => {
  let actions: Observable<any>;
  let effects: CategoriesSearchEffects;
  const mockCategoriesSearchHttpService = createSpyObj('CategoriesSearchHttpService', ['GetCategoriesByFiltersAsync']);

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        CategoriesSearchEffects,
        provideMockActions(() => actions),
        {provide: CategoriesSearchHttpService, useValue: mockCategoriesSearchHttpService},
      ]
    });

    effects = TestBed.inject(CategoriesSearchEffects);
  });

  it('should return a success action with CategoryBasic[]', () => {
    // fake return objects, category basic and envelope
    const categoryBasic1 = CategoryBasicBuilder.basic().build();
    const categoryBasic2 = CategoryBasicBuilder.basic().build();
    const categoryBasicsResults: CategoryBasic[] = [categoryBasic1, categoryBasic2];
    const envelope = new Envelope<ICategoryBasic[]>();
    envelope.result = categoryBasicsResults;

    // marble setup
    const response = cold('-a|', {a: envelope});
    mockCategoriesSearchHttpService.GetCategoriesByFiltersAsync.mockReturnValue(response);
    actions = hot('-a', {a: new CategoriesFilter(new CategorySearchFilters())});

    const expected = cold('--b', {b: new CategoriesFilterSuccess(categoryBasicsResults)});
    expect(effects.searchCategories$).toBeObservable(expected);
  });
  it('should fail and return an action with the error', () => {
    const errorMessage = 'server error';
    const error = new Error(errorMessage);

    actions = hot('-a', {a: new CategoriesFilter(new CategorySearchFilters())});
    const response = cold('-#|', {}, error);
    mockCategoriesSearchHttpService.GetCategoriesByFiltersAsync.mockReturnValue(response);

    const expected = cold('--b', {b: new searchActions.CategoriesFilterFail(error.message)});
    expect(effects.searchCategories$).toBeObservable(expected);
  });
  it('should return an error when server returns Envelope with errorMessage', () => {
    // fake return objects, category basic and envelope
    const errorMessage = 'envelope contains error message';
    const envelope = new Envelope<ICategoryBasic[]>();
    envelope.errorMessage = errorMessage;

    // marble setup
    const response = cold('-a|', {a: envelope});
    mockCategoriesSearchHttpService.GetCategoriesByFiltersAsync.mockReturnValue(response);
    actions = hot('-a', {a: new CategoriesFilter(new CategorySearchFilters())});

    const expected = cold('--b', {b: new CategoriesFilterFail(errorMessage)});
    expect(effects.searchCategories$).toBeObservable(expected);
  });

});
