import {Component, OnInit} from '@angular/core';

import {ActivatedRoute, Router} from '@angular/router';
import {CategoriesService} from '../categories/categories.service';
import {INewField, NewField} from '../models/NewField';

@Component({
  selector: 'app-field-edit',
  templateUrl: './field-edit.component.html',
  styleUrls: ['./field-edit.component.scss']
})
export class FieldEditComponent implements OnInit {


  model = new NewField({ name: '', description: '', rootCategoryId: 0});

  submitted = false;

  constructor(public router: Router, private categoriesService: CategoriesService, private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.model.rootCategoryId = Number(this.route.snapshot.paramMap.get('categoryId'));
  }

  onSubmit() {
    this.submitted = true;
    this.categoriesService.addField(this.model)
      .subscribe((data: INewField) => {
        this.router.navigate(['/categoryList']);
      });

  }

}
