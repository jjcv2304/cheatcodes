import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {Category} from '../category.model';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss']
})
export class CategoryListComponent implements OnInit {
  @Output() categoryWasSelected = new EventEmitter<Category>();
  categories: Category[] = [
    new Category('Angular', 'Notes to remember Angular ideas'),
    new Category('FoodStorage', 'Grouping to know available food at home'),
    new Category('Hikes', 'Hikes to be classified by area')
  ];

  constructor() {
  }

  ngOnInit() {
  }

  onCategorySelected(category: Category) {
    this.categoryWasSelected.emit(category);
  }
}
