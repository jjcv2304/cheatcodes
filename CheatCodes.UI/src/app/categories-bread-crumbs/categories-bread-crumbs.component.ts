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
export class CategoriesBreadCrumbsComponent implements OnChanges {
  breadCrumbsText = this.GetDefaultBreadCrumbsText();
  @Input() categoryId: number;

  categoryUrl = 'https://localhost:44326/api/categories';

  constructor(private http: HttpClient) {
  }

  ngOnChanges() {
    // this will fire every time categoryId Observer pushes a new value
    this.breadCrumbsText = this.GetDefaultBreadCrumbsText();
    if (this.categoryId < 1) {
      return;
    }
    this.getCategoryBreadCrumbs(this.categoryId).subscribe(bc => {
      this.breadCrumbsText = this.buildBreadCrumbsText(bc);
    });
  }

  private GetDefaultBreadCrumbsText() {
    return '...';
  }

  private getCategoryBreadCrumbs(categoryId: number): Observable<CategoryBreadCrumb> {

    function handleError(methodName: string, err: any) {
      console.log('Error on ' + methodName);
      return EMPTY;
    }

    return this.http.get<Envelope<CategoryBreadCrumb>>(this.categoryUrl + '/GetBreadCrumbs/' + categoryId)
      .pipe(
        map(res => {
          if (res.errorMessage !== null && res.errorMessage !== undefined) {
            handleError('getCategoryBreadCrumbs', res);
          }
          return res.result;
        }),
        catchError(err => handleError('getCategoryBreadCrumbs', err))
      );
  }

  private buildBreadCrumbsText(breadCrumbRoot: CategoryBreadCrumb): string {
    let breadCrumbText = breadCrumbRoot.name + ' > ';

    if (breadCrumbRoot.child != null) {
      breadCrumbText += (this.buildBreadCrumbsText(breadCrumbRoot.child));
    }
    return breadCrumbText;
  }

}
