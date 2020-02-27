import {Component, OnDestroy, OnInit} from '@angular/core';
import {BreakpointObserver, Breakpoints} from '@angular/cdk/layout';
import {Observable} from 'rxjs';
import {map, shareReplay} from 'rxjs/operators';
import {CategoryBasic} from '../model/category';
import {CategoriesSearchHttpService} from '../categories-search-http.service';

@Component({
  selector: 'app-categories-search-container',
  templateUrl: './categories-search-container.component.html',
  styleUrls: ['./categories-search-container.component.scss']
})
export class CategoriesSearchContainerComponent implements OnInit, OnDestroy {
  showDetailView = false;

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );
  cardsSearchResult: CategoryBasic[];

  constructor(private breakpointObserver: BreakpointObserver, private categoriesSearchService: CategoriesSearchHttpService) {
  }

  ngOnInit(): void {
    this.categoriesSearchService.newCardSearchResult.subscribe({
      next: (v) => {
        if (v === true) {
          this.cardsSearchResult = this.categoriesSearchService.currentCategories;
        } else {
          this.cardsSearchResult = null;
        }
      }
    });
  }

  ngOnDestroy(): void {
    this.categoriesSearchService.newCardSearchResult.unsubscribe();
  }

}
