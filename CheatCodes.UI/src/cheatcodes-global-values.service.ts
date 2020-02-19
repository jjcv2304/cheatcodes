import {Injectable} from '@angular/core';
import {BehaviorSubject, Observable, of, Subject} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CheatCodesGlobalValuesService {
  private showBasicMenu = new BehaviorSubject<boolean>(true);

  constructor() {
  }

  setShowBasicMenu(showBasicMenu: boolean) {
    this.showBasicMenu.next(showBasicMenu);
  }

  getShowBasicMenu(): Observable<boolean> {
    return this.showBasicMenu.asObservable();
  }
}
