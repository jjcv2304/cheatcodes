import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CardMoveMenuComponent } from './card-move-menu.component';

describe('CardMoveMenuComponent', () => {
  let component: CardMoveMenuComponent;
  let fixture: ComponentFixture<CardMoveMenuComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CardMoveMenuComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CardMoveMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
