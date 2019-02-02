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
    return this.http.get<Envelope<Array<ICategory>>>(this.categoryUrl);
  };

  getAllCategories_InMemory() : any {
   // return this.http.get<ICategory>(this.categoryUrl);

    let card1 = { name:'card1', description:'card 1 desc', order: 1, color: 'rgba(0, 255, 255, 0.2)'};
    let card2 = { name:'card2', description:'card 2 desc', order: 2, color: 'rgba(0, 255, 0, 0.2)'};
    let card3 = { name:'card5', description:'card 5 desc', order: 5, color: 'rgba(0, 0, 255, 0.2'};
    let card4 = { name:'card4', description:'card 4 desc', order: 4, color: 'rgba(255, 255, 0, 0.2)'};
    let card5 = { name:'card3', description:'card 3 desc', order: 3, color: 'rgba(255, 0, 255, 0.2)'};

    let card6 = { name:'card6', description:'card 6 desc', order: 6, color: 'rgba(125, 125, 0, 0.2'};
    let card7 = { name:'card7', description:'card 7 desc', order: 7, color: 'rgba(0, 65, 65, 0.2)'};
    let card8 = { name:'card8', description:'card 8 desc', order: 8, color: 'rgba(0, 0, 125, 0.2)'};

    let cards : any;
    cards = [];
    cards.push(card1);
    cards.push(card2);
    cards.push(card4);
    cards.push(card5);
    cards.push(card3);

    cards.push(card6);
    cards.push(card7);
    cards.push(card8);
    cards.sort(function (a, b) {
      if (a.order > b.order) {
        return 1;
      }
      if (a.order < b.order) {
        return -1;
      }
      // a must be equal to b
      return 0;
    });
    return cards;
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

