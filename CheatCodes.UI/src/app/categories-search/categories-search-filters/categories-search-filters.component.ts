import { Component } from "@angular/core";
import { FormControl, FormGroup } from "@angular/forms";
import { BreakpointObserver } from "@angular/cdk/layout";
import { CategoriesSearchHttpService } from "../categories-search-http.service";
import { CategorySearchFilters } from "../model/categorySearchFilters";

@Component({
  selector: "app-categories-search-filters",
  templateUrl: "./categories-search-filters.component.html",
  styleUrls: ["./categories-search-filters.component.scss"]
})
export class CategoriesSearchFiltersComponent {


  searchCategoriesForm = new FormGroup({
    categoryNameFilter: new FormControl(""),
    categoryName2Filter: new FormControl("")
  });

  categoryNameFilterAnd = false;
  categoryNameFilterOr = false;

  constructor(private breakpointObserver: BreakpointObserver, private apiService: CategoriesSearchHttpService) {
    this.searchCategoriesForm.get("categoryName2Filter").disable();
  }

  Search() {
    const filters = new CategorySearchFilters();
    filters.categoryNameFilter = this.searchCategoriesForm.get("categoryNameFilter").value;
    if (this.categoryNameFilterAnd || this.categoryNameFilterOr) {
      filters.categoryNameFilterAnd = this.categoryNameFilterAnd;
      filters.categoryNameFilterOr = this.categoryNameFilterOr;
      filters.categoryName2Filter = this.searchCategoriesForm.get("categoryName2Filter").value;
    }
    this.apiService.GetCategoriesByFiltersAsync(filters);
  }

  categoryNameFilterAndClicked() {
    this.categoryNameFilterOr = false;
  }

  categoryNameFilterOrClicked() {
    this.categoryNameFilterAnd = false;
  }

}
