import {Component, OnInit} from '@angular/core';
import {CategoriesService} from '../categories/categories.service';
import {Category, ICategory} from '../models/category';
import {Envelope} from '../models/envelope';
import {CategoryFilter} from '../models/CategoryFilter';
import {ActivatedRoute, Router} from '@angular/router';
import {CheatCodesGlobalValuesService} from '../../../cheatcodes-global-values.service';

@Component({
  selector: 'app-categories-list',
  templateUrl: './categories-list.component.html',
  styleUrls: ['./categories-list.component.scss']
})
export class CategoriesListComponent implements OnInit {

  get cards(): Category[] {
    return this.categoriesService.currentCategories;
  }

  constructor(private categoriesService: CategoriesService, private route: ActivatedRoute, public router: Router,
              private globalValues: CheatCodesGlobalValuesService) {
    this.globalValues.setShowBasicMenu(true);
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
    const parentId = this.categoriesService.currentCategories[0].parentId || 0;
    this.router.navigateByUrl('/categoryEdit/parentId/' + parentId);
  }
}


