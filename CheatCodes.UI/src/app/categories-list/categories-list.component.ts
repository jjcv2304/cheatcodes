import {Component, OnInit} from '@angular/core';
import {CategoriesService} from "../categories/categories.service";
import {Category, ICategory} from "../models/category";
import {Envelope} from "../models/envelope";

@Component({
  selector: 'app-categories-list',
  templateUrl: './categories-list.component.html',
  styleUrls: ['./categories-list.component.scss']
})
export class CategoriesListComponent implements OnInit {
  public category: ICategory;
  cards : any;
  constructor(private categoriesService:CategoriesService) { }

  ngOnInit() {
    let card1 = { name:'card1', description:'card 1 desc', order: 1, color: 'rgba(0, 255, 255, 0.2)'};
    let card2 = { name:'card2', description:'card 2 desc', order: 2, color: 'rgba(0, 255, 0, 0.2)'};
    let card3 = { name:'card5', description:'card 5 desc', order: 5, color: 'rgba(0, 0, 255, 0.2'};
    let card4 = { name:'card4', description:'card 4 desc', order: 4, color: 'rgba(255, 255, 0, 0.2)'};
    let card5 = { name:'card3', description:'card 3 desc', order: 3, color: 'rgba(255, 0, 255, 0.2)'};

    let card6 = { name:'card6', description:'card 6 desc', order: 6, color: 'rgba(125, 125, 0, 0.2'};
    let card7 = { name:'card7', description:'card 7 desc', order: 7, color: 'rgba(0, 65, 65, 0.2)'};
    let card8 = { name:'card8', description:'card 8 desc', order: 8, color: 'rgba(0, 0, 125, 0.2)'};
    this.cards = [];
    this.cards.push(card1);
    this.cards.push(card2);
    this.cards.push(card4);
    this.cards.push(card5);
    this.cards.push(card3);

    this.cards.push(card6);
    this.cards.push(card7);
    this.cards.push(card8);
    this.cards.sort(function (a, b) {
      if (a.order > b.order) {
        return 1;
      }
      if (a.order < b.order) {
        return -1;
      }
      // a must be equal to b
      return 0;
    });
  }

  getCategory() {
    this.categoriesService.getById(4)
      .subscribe((data: Envelope<ICategory>) => {
        console.dir(data.result);
        this.category = { ...data.result };
      });
  }

  addCategory() {
    let category = new Category({
      description: ' new desd',
      name : 'new name'
    });

    this.categoriesService.addCategory(category)
      .subscribe((data: ICategory) => {
        this.category = { ...data };
      });
  }


}


