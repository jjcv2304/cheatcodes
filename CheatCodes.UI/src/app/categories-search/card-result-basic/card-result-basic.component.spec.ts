import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CardResultBasicComponent } from './card-result-basic.component';

describe('CardResultBasicComponent', () => {
  let component: CardResultBasicComponent;
  let fixture: ComponentFixture<CardResultBasicComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CardResultBasicComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CardResultBasicComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
