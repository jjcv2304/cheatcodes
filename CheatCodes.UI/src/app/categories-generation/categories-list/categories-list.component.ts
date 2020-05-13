import {Component, OnInit} from '@angular/core';
import {CategoriesService} from '../categories/categories.service';
import {Category} from '../models/category';
import {CategoryFilter} from '../models/CategoryFilter';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
  selector: 'app-categories-list',
  templateUrl: './categories-list.component.html',
  styleUrls: ['./categories-list.component.scss']
})
export class CategoriesListComponent implements OnInit {

  get cards(): Category[] {
    return this.categoriesService.GetCurrentCategories();
  }

  constructor(private categoriesService: CategoriesService, private route: ActivatedRoute, public router: Router) {
  }

  ngOnInit() {
    const reload = Boolean(this.route.snapshot.paramMap.get('reload'));
    if (reload === true) {
      this.categoriesService.RefreshCategoryLastFilter();
    } else {
      this.categoriesService.SetCategoryFilter(new CategoryFilter());
    }
  }

  addSiblingCard() {
    const parentId = this.categoriesService.GetCurrentCategories()[0].parentId || 0;
    this.router.navigateByUrl(`/categoryEdit/parentId/${parentId}`);
  }
}
