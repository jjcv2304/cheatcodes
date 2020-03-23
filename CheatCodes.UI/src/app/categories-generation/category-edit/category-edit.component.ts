import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from "@angular/core";
import { Category } from "../models/category";
import { ActivatedRoute, Router } from "@angular/router";
import { CategoriesService } from "../categories/categories.service";
import { CategoryFilter } from "../models/CategoryFilter";

@Component({
  selector: "app-category-edit",
  templateUrl: "./category-edit.component.html",
  styleUrls: ["./category-edit.component.scss"]
})
export class CategoryEditComponent implements OnInit, AfterViewInit {
  @ViewChild("fieldName", { static: false })
  fieldName: ElementRef;
  model = new Category({ id: 0, name: "", description: "", parentId: 0 });

  submitted = false;

  constructor(public router: Router, private categoriesService: CategoriesService, private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.model.parentId = Number(this.route.snapshot.paramMap.get("parentId"));
  }

  ngAfterViewInit(): void {
    this.fieldName.nativeElement.focus();
  }

  onSubmit() {
    this.submitted = true;
    this.categoriesService.addCategory(this.model)
      .subscribe(() => {
        if (this.model.parentId === 0) {
          this.router.navigate(["/categoryList"]);
        } else {
          const newFilter = CategoryFilter.FilterByParent(this.model.parentId);
          this.categoriesService.SetCategoryFilter(newFilter);
          this.router.navigate(["/categoryList/true"]);
        }
      });
  }
}
