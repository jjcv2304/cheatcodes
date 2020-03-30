import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from "@angular/core";

import { ActivatedRoute, Router } from "@angular/router";
import { CategoriesService } from "../categories/categories.service";
import { INewField, NewField } from "../models/NewField";

@Component({
  selector: "app-field-edit",
  templateUrl: "./field-edit.component.html",
  styleUrls: ["./field-edit.component.scss"]
})
export class FieldEditComponent implements OnInit, AfterViewInit {

  @ViewChild("fieldName")
  fieldName: ElementRef;
  model = new NewField({ name: "", description: "", categoryId: 0 });

  submitted = false;

  constructor(public router: Router, private categoriesService: CategoriesService, private route: ActivatedRoute) {

  }

  ngOnInit(): void {
    this.model.categoryId = Number(this.route.snapshot.paramMap.get("categoryId"));
  }

  ngAfterViewInit(): void {
    this.fieldName.nativeElement.focus();
  }

  onSubmit() {
    this.submitted = true;
    this.categoriesService.addField(this.model)
      .subscribe((data: INewField) => {
        this.router.navigate(["/categoryList/true"]);
      });

  }


}
