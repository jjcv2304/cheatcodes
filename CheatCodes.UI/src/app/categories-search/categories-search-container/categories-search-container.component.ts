import {Component} from '@angular/core';
import {BreakpointObserver, Breakpoints} from '@angular/cdk/layout';
import {Observable} from 'rxjs';
import {map, shareReplay} from 'rxjs/operators';
import {CheatCodesGlobalValuesService} from '../../../cheatcodes-global-values.service';

@Component({
  selector: 'app-categories-search-container',
  templateUrl: './categories-search-container.component.html',
  styleUrls: ['./categories-search-container.component.scss']
})
export class CategoriesSearchContainerComponent {

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  constructor(private breakpointObserver: BreakpointObserver, private globalValues: CheatCodesGlobalValuesService) {
    this.globalValues.setShowBasicMenu(false);
  }

}
