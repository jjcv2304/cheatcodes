import {Injectable} from '@angular/core';
import {Envelope} from '../utils/envelope';
import {HttpClient, HttpParams} from '@angular/common/http';
import {CategoryBasic, CategoryTree, ICategoryBasic, ICategoryTree} from './model/category';
import {BehaviorSubject} from 'rxjs';

@Injectable()
export class CategoriesSearchHttpService {

  public newCardSearchResult = new BehaviorSubject(false);
  public newCardDetailsResult = new BehaviorSubject(false);
  private cardDetailResult: CategoryTree;
  get currentCategoryDetail(): CategoryTree {
    return this.cardDetailResult;
  }
  private cardsSearchResult: CategoryBasic[];
  get currentCategories(): CategoryBasic[] {
    return this.cardsSearchResult;
  }
  private readonly categorySearchUrl: string = '/api/CategoriesSearch';

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
    return this.http.get<Envelope<ICategoryBasic>>(
      this.categorySearchUrl + '/GetCategoriesSubTreeByRootId/' + categoryRootId)
      .subscribe(
        (data: Envelope<ICategoryTree>) => {
          this.cardDetailResult = data.result;
          this.newCardDetailsResult.next(true);
        },
        () => {
          console.log('Error GetCategoriesByPartialNameAsync');
        }
      );
  }

}


