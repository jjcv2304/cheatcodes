import {Component, OnInit} from '@angular/core';
import {CategoriesService} from '../categories/categories.service';
import {Category, ICategory} from '../models/category';
import {Envelope} from '../models/envelope';
import {CategoryFilter} from '../models/CategoryFilter';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-categories-list',
  templateUrl: './categories-list.component.html',
  styleUrls: ['./categories-list.component.scss']
})
export class CategoriesListComponent implements OnInit {

  get cards(): Category[] {
    return this.categoriesService.currentCategories;
  }

  constructor(private categoriesService: CategoriesService, private route: ActivatedRoute) {
  }

  ngOnInit() {
    const reload = Boolean(this.route.snapshot.paramMap.get('reload'));
    if (reload === true) {
      this.categoriesService.RefreshCategoryLastFilter();
    } else {
      this.categoriesService.SetCategoryFilter(new CategoryFilter());
    }
  }
}


