import {ChangeDetectionStrategy, Component, Input, OnChanges, OnInit} from '@angular/core';
import {CategoryBreadCrumb} from '../categories-generation/models/category';
import {CategoriesService} from '../categories-generation/categories/categories.service';
import {HttpClient} from '@angular/common/http';
import {EMPTY, Observable} from 'rxjs';
import {Envelope} from '../utils/envelope';
import {catchError, map} from 'rxjs/operators';

@Component({
  selector: 'app-categories-bread-crumbs',
  templateUrl: './categories-bread-crumbs.component.html',
  styleUrls: ['./categories-bread-crumbs.component.scss']
})
export class CategoriesBreadCrumbsComponent implements OnChanges  {
  breadCrumbsText = '...';
  @Input() categoryId: number;

  categoryUrl = 'https://localhost:44326/api/categories';

  constructor(private http: HttpClient) {
  }

  ngOnChanges() {
    this.getCategoryBreadCrumbs(this.categoryId).subscribe(bc => {
      this.breadCrumbsText = this.buildBreadCrumbsText(bc);
    });
  }

  private getCategoryBreadCrumbs(categoryId: number): Observable<CategoryBreadCrumb> {

    return this.http.get<Envelope<CategoryBreadCrumb>>(this.categoryUrl + '/breadCrumbs/' + categoryId)
      .pipe(
        map(res => {
          if (res.errorMessage !== '') {
            this.handleError('getCategoryBreadCrumbs');
          }
          return res.result;
        }),
        catchError(err => this.handleError('getCategoryBreadCrumbs'))
      );
  }

  private handleError(methodName: string) {
    console.log('Error on ' + methodName);
    return EMPTY;
  }

  private buildBreadCrumbsText(breadCrumbRoot: CategoryBreadCrumb): string {
    let breadCrumbText = breadCrumbRoot.name;

    if (breadCrumbRoot.child != null) {
      breadCrumbText += ('>' + this.buildBreadCrumbsText(breadCrumbRoot.child));
    }
    return breadCrumbText;
  }

}
