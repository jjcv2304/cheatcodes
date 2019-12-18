import {Component, OnInit} from '@angular/core';
import {CategorySummary} from './category-summary.model';

@Component({
  selector: 'app-categories-summary',
  templateUrl: './categories-summary.component.html',
  styleUrls: ['./categories-summary.component.scss']
})
export class CategoriesSummaryComponent implements OnInit {
  categorySummary: CategorySummary = new CategorySummary(5, 12, 8);

  constructor() {
  }

  ngOnInit() {
  }

}
