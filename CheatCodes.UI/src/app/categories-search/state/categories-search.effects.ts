import {Injectable} from '@angular/core';
import {CategoriesSearchHttpService} from '../categories-search-http.service';
import {Actions, Effect, ofType} from '@ngrx/effects';
import * as searchActions from './categories-search.actions';
import {catchError, map, switchMap} from 'rxjs/operators';
import {CategoryBasic, ICategoryBasic} from '../model/category';
import {Envelope} from '../../utils/envelope';
import {Observable, of, throwError} from 'rxjs';
import {Action} from '@ngrx/store';
import {CategoriesFilter} from './categories-search.actions';


@Injectable()
export class CategoriesSearchEffects {
  constructor(private actions$: Actions, private categoriesSearchHttpService: CategoriesSearchHttpService) {
  }

  // GetCategoriesByFiltersAsync returns an envelope....
  @Effect()
  searchCategories$: Observable<Action> = this.actions$.pipe(
    ofType(searchActions.CategoriesSearchActionTypes.CategoriesFilter),
    switchMap((action: CategoriesFilter) => {
      return this.categoriesSearchHttpService.GetCategoriesByFiltersAsync(action.payload).pipe(
        map((data: Envelope<Array<ICategoryBasic>>) => {
          if (data.errorMessage === '') {
            return new searchActions.CategoriesFilterSuccess(data.result);
          } else {
            return new searchActions.CategoriesFilterFail(data.errorMessage);
          }
        }),
        catchError(err =>
          of(new searchActions.CategoriesFilterFail(err.message)))
      );
    })
  );
}
