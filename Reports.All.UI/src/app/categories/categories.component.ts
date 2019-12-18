import { Component, OnInit } from '@angular/core';
import {Category} from './category.model';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.scss']
})
export class CategoriesComponent implements OnInit {
  categorySelected: Category;
  constructor() { }

  ngOnInit() {
  }

}
