import {Component} from '@angular/core';
import {Category, ICategory} from "../models/category";
import {Router} from '@angular/router';
import {CategoriesService} from "../categories/categories.service";

@Component({
  selector: 'app-category-edit',
  templateUrl: './category-edit.component.html',
  styleUrls: ['./category-edit.component.scss']
})
export class CategoryEditComponent {

  model = new Category({id: 0, name: ""});

  submitted = false;

  constructor(public router: Router, private categoriesService: CategoriesService) {
  }

  onSubmit() {
    this.submitted = true;
    this.categoriesService.addCategory(this.model)
      .subscribe((data: ICategory) => {
        this.router.navigate(['/categoryList']);
      });



  }

  newCategory() {
    this.model = new Category({id: 0, name: "", description: ""});
  }

}
