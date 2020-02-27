import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CardResultDetailComponent } from './card-result-detail.component';

describe('CardResultDetailComponent', () => {
  let component: CardResultDetailComponent;
  let fixture: ComponentFixture<CardResultDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CardResultDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CardResultDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
