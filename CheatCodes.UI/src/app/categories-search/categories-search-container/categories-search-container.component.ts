import {Component, OnDestroy, OnInit} from '@angular/core';
import {BreakpointObserver, Breakpoints} from '@angular/cdk/layout';
import {Observable, Subscription} from 'rxjs';
import {map, shareReplay} from 'rxjs/operators';
import {CategoryBasic, CategoryTree} from '../model/category';
import {CategoriesSearchHttpService} from '../categories-search-http.service';

@Component({
  selector: 'app-categories-search-container',
  templateUrl: './categories-search-container.component.html',
  styleUrls: ['./categories-search-container.component.scss']
})
export class CategoriesSearchContainerComponent implements OnInit, OnDestroy {
  showDetailView = false;
  newCardSearchResultSubscription: Subscription;
  newCardDetailsResultSubscription: Subscription;

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );
  cardsSearchResult: CategoryBasic[];
  cardDetails: CategoryTree;

  constructor(private breakpointObserver: BreakpointObserver, private categoriesSearchService: CategoriesSearchHttpService) {
  }

  ngOnInit(): void {

    this.newCardSearchResultSubscription = this.categoriesSearchService.newCardSearchResult.subscribe({
      next: (v) => {
        if (v === true) {
          this.showDetailView = false;
          this.cardsSearchResult = this.categoriesSearchService.currentCategories;
        } else {
          this.cardsSearchResult = null;
          this.showDetailView = true;
        }
      }
    });

    this.newCardDetailsResultSubscription = this.categoriesSearchService.newCardDetailsResult.subscribe({
      next: (v) => {
        if (v === true) {
          this.cardDetails = this.categoriesSearchService.currentCategoryDetail;
          this.showDetailView = true;
        } else {
          this.showDetailView = false;
          this.cardDetails = null;
        }
      }
    });

  }

  ngOnDestroy(): void {
    if (this.newCardSearchResultSubscription) {
      this.newCardSearchResultSubscription.unsubscribe();
    }
    if (this.newCardDetailsResultSubscription) {
      this.newCardDetailsResultSubscription.unsubscribe();
    }
  }

}
