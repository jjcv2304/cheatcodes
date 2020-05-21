import {Component, OnDestroy, OnInit} from '@angular/core';
import {BreakpointObserver, Breakpoints} from '@angular/cdk/layout';
import {Observable, Subscription} from 'rxjs';
import {map, shareReplay, takeWhile} from 'rxjs/operators';
import {CategoryBasic, CategoryTree} from '../model/category';
import {CategoriesSearchHttpService} from '../categories-search-http.service';
import {select, Store} from '@ngrx/store';
import * as fromCategorySearch from '../state';

@Component({
  selector: 'app-categories-search-container',
  templateUrl: './categories-search-container.component.html',
  styleUrls: ['./categories-search-container.component.scss']
})
export class CategoriesSearchContainerComponent implements OnInit, OnDestroy {
  showDetailView = false;
  componentActive = true;
  newCardDetailsResultSubscription: Subscription;

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );
  cardsSearchResult: CategoryBasic[];
  cardDetails: CategoryTree;
  currentParentId: number;

  constructor(private breakpointObserver: BreakpointObserver,
              private categoriesSearchService: CategoriesSearchHttpService,
              private store: Store<fromCategorySearch.State>) {
    this.currentParentId = 0;
  }

  ngOnInit(): void {

    this.store.pipe(select(fromCategorySearch.getFilteredCategories),
      takeWhile(() => this.componentActive))
      .subscribe((result: CategoryBasic[]) => this.cardsSearchResult = result);

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
    this.componentActive = false;
    if (this.newCardDetailsResultSubscription) {
      this.newCardDetailsResultSubscription.unsubscribe();
    }
  }

}
