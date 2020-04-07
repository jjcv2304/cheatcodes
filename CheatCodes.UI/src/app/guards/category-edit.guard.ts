import {Injectable} from '@angular/core';
import {CanDeactivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree} from '@angular/router';
import {Observable} from 'rxjs';
import {CategoriesListComponent} from '../categories-generation/categories-list/categories-list.component';
import {CategoryEditComponent} from '../categories-generation/category-edit/category-edit.component';


@Injectable({
  providedIn: 'root'
})
export class CategoryEditGuard implements CanDeactivate<CategoryEditComponent> {
  canDeactivate(
    component: CategoryEditComponent,
    currentRoute: ActivatedRouteSnapshot,
    currentState: RouterStateSnapshot,
    nextState?: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    if (component.isDirty) {
      return confirm(`Navigate awy and lose all changes? `);
    }
    return true;
  }

}
