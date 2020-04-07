import {Injectable} from '@angular/core';
import {CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router, CanLoad, UrlSegment, Route} from '@angular/router';
import {Observable, of} from 'rxjs';
import {AuthService} from '../security/auth-service.component';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class LoggedInGuard implements CanActivate, CanLoad {

  constructor(private authService: AuthService, private router: Router) {
  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.checkLoggedId();
  }

  checkLoggedId(): Promise<boolean> {
    return this.authService.isLoggedIn().then(loggedIn => {
      if (loggedIn) {
        return (true);
      }
      this.authService.login();
      return (false);
    });
  }

  canLoad(route: Route, segments: UrlSegment[]): Observable<boolean> | Promise<boolean> | boolean {
    return this.checkLoggedId();
  }

}
