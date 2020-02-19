import {Component, OnDestroy, OnInit} from '@angular/core';
import {CheatCodesGlobalValuesService} from '../../cheatcodes-global-values.service';
import {Subscription} from 'rxjs';

@Component({
  selector: 'app-main-menu',
  templateUrl: './main-menu.component.html',
  styleUrls: ['./main-menu.component.scss']
})
export class MainMenuComponent implements OnInit, OnDestroy {
  showMenu: boolean;
  subscription: Subscription;

  constructor(private globalValues: CheatCodesGlobalValuesService) {

  }

  ngOnInit(): void {
    this.subscription = this.globalValues.getShowBasicMenu().subscribe(show =>
      this.showMenu = show
    );
  }

  ngOnDestroy() {
    // unsubscribe to ensure no memory leaks
    this.subscription.unsubscribe();
  }

}
