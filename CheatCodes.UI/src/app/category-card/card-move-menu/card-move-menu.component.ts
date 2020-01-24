import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-card-move-menu',
  templateUrl: './card-move-menu.component.html',
  styleUrls: ['./card-move-menu.component.scss']
})
export class CardMoveMenuComponent implements OnInit {
  submenuVisible = false;
  @Input() canMoveUp: boolean;
  @Output() moveUp: EventEmitter<void> = new EventEmitter();

  constructor() { }

  ngOnInit() {
  }

  showSubmenu() {
    this.submenuVisible = true;
  }

  hideSubmenu() {
    this.submenuVisible = false;
  }
  moveUpEvent() {
    this.moveUp.emit();
  }

}
