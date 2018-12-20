import {Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse, HttpHeaders} from '@angular/common/http';
import {Category, ICategory} from "../models/category";
import { Observable } from 'rxjs';
import {Envelope} from "../models/envelope";

@Injectable()
export class CategoriesService {

  readonly categoryUrl: string;
  readonly httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private http: HttpClient) {
    this.categoryUrl = '/api/categories';
  };

  getAllCategories() {
    return this.http.get<ICategory>(this.categoryUrl);
  };

  getById(categoryId: number) {
    return this.http.get<Envelope<ICategory>>(this.categoryUrl + '/' + categoryId);
  };

  addCategory (category: Category) : Observable<Category> {
    return this.http.post<Category>(this.categoryUrl, category, this.httpOptions);
   // return this.http.post<Category>(this.categoryUrl, category);
  };

//todo add error handling
}

