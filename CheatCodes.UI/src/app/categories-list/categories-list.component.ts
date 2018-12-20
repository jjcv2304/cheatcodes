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

  constructor(private categoriesService:CategoriesService) { }

  ngOnInit() {
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


