import {Injectable} from '@angular/core';
import {Envelope} from '../utils/envelope';
import {HttpClient, HttpParams} from '@angular/common/http';
import {CategoryBasic, ICategoryBasic} from './model/category';
import {BehaviorSubject} from 'rxjs';

@Injectable()
export class CategoriesSearchHttpService {

  public newCardSearchResult = new BehaviorSubject(false);
  public newCardDetailsResult = new BehaviorSubject(false);

  private readonly categorySearchUrl: string = '/api/CategoriesSearch';
  private cardsSearchResult: CategoryBasic[];

  get currentCategories(): CategoryBasic[] {
    return this.cardsSearchResult;
  }

  constructor(private http: HttpClient) {
  }

  public GetCategoriesByPartialNameAsync(categoryName: string) {
    let params = new HttpParams();
    params = params.append('textSearch', categoryName);
    return this.http.get<Envelope<Array<ICategoryBasic>>>(
      this.categorySearchUrl + '/GetCategoriesByPartialName', {params: params})
      .subscribe(
        (data: Envelope<Array<ICategoryBasic>>) => {
          this.cardsSearchResult = data.result;
          this.newCardSearchResult.next(true);
        },
        () => {
          console.log('Error GetCategoriesByPartialNameAsync');
        }
      );
  }
  public GetCategoryDetails(categoryRootId: number) {
    return this.http.get<Envelope<Array<ICategoryBasic>>>(
      this.categorySearchUrl + '/GetCategoriesSubTreeByRootId/' + categoryRootId)
      .subscribe(
        (data: Envelope<Array<ICategoryBasic>>) => {
          this.cardsSearchResult = data.result;
          this.newCardSearchResult.next(true);
        },
        () => {
          console.log('Error GetCategoriesByPartialNameAsync');
        }
      );
  }

}


