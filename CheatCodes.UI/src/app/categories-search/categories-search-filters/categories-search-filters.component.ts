import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup} from '@angular/forms';
import {BreakpointObserver} from '@angular/cdk/layout';
import {CategoriesSearchHttpService} from '../categories-search-http.service';

@Component({
  selector: 'app-categories-search-filters',
  templateUrl: './categories-search-filters.component.html',
  styleUrls: ['./categories-search-filters.component.scss']
})
export class CategoriesSearchFiltersComponent {


  searchCategoriesForm = new FormGroup({
    categoryNameFilter: new FormControl(''),
  });

  constructor(private breakpointObserver: BreakpointObserver, private apiService: CategoriesSearchHttpService) {
  }

  Search() {
    const categoryNameFilter = this.searchCategoriesForm.get('categoryNameFilter').value;
    this.apiService.GetCategoriesByPartialNameAsync(categoryNameFilter);
  }

}
